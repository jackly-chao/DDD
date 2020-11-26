using CApplication.IService;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CExtension.Permission
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        /// <summary>
        /// 验证方案提供对象
        /// </summary>
        public IAuthenticationSchemeProvider Schemes { get; set; }
        private readonly IPermissionService _roleModulePermissionServices;
        private readonly IHttpContextAccessor _accessor;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="schemes"></param>
        /// <param name="roleModulePermissionServices"></param>
        /// <param name="accessor"></param>
        public PermissionHandler(IAuthenticationSchemeProvider schemes, IPermissionService roleModulePermissionServices, IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            Schemes = schemes;
            _roleModulePermissionServices = roleModulePermissionServices;
        }

        // 重写异步处理程序
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var httpContext = _accessor.HttpContext;

            // 获取系统中所有的角色和菜单的关系集合
            if (!requirement.Permissions.Any())
            {
                var data = await _roleModulePermissionServices.GetAllByUserIdAsync(1);
                var list = new List<PermissionItem>();
                // ids4和jwt切换
                // ids4
                if (true)
                {
                    list = (from item in data
                            where !string.IsNullOrEmpty(item.PermissionName)
                            orderby item.Id
                            select new PermissionItem
                            {
                                Url = item.PermissionPath,
                                Role = item.Id.ToString(),
                            }).ToList();
                }
                // jwt
                //else
                //{
                //    list = (from item in data
                //            where item.IsDeleted == false
                //            orderby item.Id
                //            select new PermissionItem
                //            {
                //                Url = item.Module?.LinkUrl,
                //                Role = item.Role?.Name.ObjToString(),
                //            }).ToList();
                //}
                requirement.Permissions = list;
            }

            if (httpContext != null)
            {
                var questUrl = httpContext.Request.Path.Value.ToLower();

                // 整体结构类似认证中间件UseAuthentication的逻辑，具体查看开源地址
                // https://github.com/dotnet/aspnetcore/blob/master/src/Security/Authentication/Core/src/AuthenticationMiddleware.cs
                httpContext.Features.Set<IAuthenticationFeature>(new AuthenticationFeature
                {
                    OriginalPath = httpContext.Request.Path,
                    OriginalPathBase = httpContext.Request.PathBase
                });

                // Give any IAuthenticationRequestHandler schemes a chance to handle the request
                // 主要作用是: 判断当前是否需要进行远程验证，如果是就进行远程验证
                var handlers = httpContext.RequestServices.GetRequiredService<IAuthenticationHandlerProvider>();
                foreach (var scheme in await Schemes.GetRequestHandlerSchemesAsync())
                {
                    if (await handlers.GetHandlerAsync(httpContext, scheme.Name) is IAuthenticationRequestHandler handler && await handler.HandleRequestAsync())
                    {
                        context.Fail();
                        return;
                    }
                }


                //判断请求是否拥有凭据，即有没有登录
                var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
                if (defaultAuthenticate != null)
                {
                    var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                    //result?.Principal不为空即登录成功
                    if (result?.Principal != null)
                    {

                        httpContext.User = result.Principal;

                        // 获取当前用户的角色信息
                        var currentUserRoles = new List<string>();
                        // ids4和jwt切换
                        // ids4
                        if (true)
                        {
                            currentUserRoles = (from item in httpContext.User.Claims
                                                where item.Type == "role"
                                                select item.Value).ToList();
                        }
                        else
                        {
                            // jwt
                            //currentUserRoles = (from item in httpContext.User.Claims
                            //                    where item.Type == requirement.ClaimType
                            //                    select item.Value).ToList();
                        }

                        var isMatchRole = false;
                        var permisssionRoles = requirement.Permissions.Where(w => currentUserRoles.Contains(w.Role));
                        foreach (var item in permisssionRoles)
                        {
                            try
                            {
                                if (Regex.Match(questUrl, item.Url.ToLower())?.Value == questUrl)
                                {
                                    isMatchRole = true;
                                    break;
                                }
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                        }

                        //验证权限
                        if (currentUserRoles.Count <= 0 || !isMatchRole)
                        {
                            context.Fail();
                            return;
                        }

                        var isExp = false;
                        // ids4和jwt切换
                        // ids4
                        if (true)
                        {
                            isExp = (httpContext.User.Claims.SingleOrDefault(s => s.Type == "exp")?.Value) != null/* && DateHelper.StampToDateTime(httpContext.User.Claims.SingleOrDefault(s => s.Type == "exp")?.Value) >= DateTime.Now*/;
                        }
                        else
                        {
                            // jwt
                            //isExp = (httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration)?.Value) != null && DateTime.Parse(httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration)?.Value) >= DateTime.Now;
                        }
                        if (isExp)
                        {
                            context.Succeed(requirement);
                        }
                        else
                        {
                            context.Fail();
                            return;
                        }
                        return;
                    }
                }
                //判断没有登录时，是否访问登录的url,并且是Post请求，并且是form表单提交类型，否则为失败
                if (!(questUrl.Equals(requirement.LoginPath.ToLower(), StringComparison.Ordinal) && (!httpContext.Request.Method.Equals("POST") || !httpContext.Request.HasFormContentType)))
                {
                    context.Fail();
                    return;
                }
            }

            //context.Succeed(requirement);
        }
    }

    public class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// 用户权限集合，一个订单包含了很多详情，
        /// 同理，一个网站的认证发行中，也有很多权限详情(这里是Role和URL的关系)
        /// </summary>
        public List<PermissionItem> Permissions { get; set; }
        /// <summary>
        /// 无权限action
        /// </summary>
        public string DeniedAction { get; set; }

        /// <summary>
        /// 认证授权类型
        /// </summary>
        public string ClaimType { internal get; set; }
        /// <summary>
        /// 请求路径
        /// </summary>
        public string LoginPath { get; set; } = "/Api/Login";
        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }
        /// <summary>
        /// 订阅人
        /// </summary>
        public string Audience { get; set; }
        /// <summary>
        /// 过期时间
        /// </summary>
        public TimeSpan Expiration { get; set; }
        /// <summary>
        /// 签名验证
        /// </summary>
        public SigningCredentials SigningCredentials { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="deniedAction">拒约请求的url</param>
        /// <param name="permissions">权限集合</param>
        /// <param name="claimType">声明类型</param>
        /// <param name="issuer">发行人</param>
        /// <param name="audience">订阅人</param>
        /// <param name="signingCredentials">签名验证实体</param>
        /// <param name="expiration">过期时间</param>
        public PermissionRequirement(string deniedAction, List<PermissionItem> permissions, string claimType, string issuer, string audience, SigningCredentials signingCredentials, TimeSpan expiration)
        {
            ClaimType = claimType;
            DeniedAction = deniedAction;
            Permissions = permissions;
            Issuer = issuer;
            Audience = audience;
            Expiration = expiration;
            SigningCredentials = signingCredentials;
        }
    }

    /// <summary>
    /// 用户或角色或其他凭据实体,就像是订单详情一样
    /// 之前的名字是 Permission
    /// </summary>
    public class PermissionItem
    {
        /// <summary>
        /// 用户或角色或其他凭据名称
        /// </summary>
        public virtual string Role { get; set; }
        /// <summary>
        /// 请求Url
        /// </summary>
        public virtual string Url { get; set; }
    }
}
