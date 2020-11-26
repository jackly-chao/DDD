using Autofac;
using CExtension.Authentication;
using CExtension.Autofac;
using CExtension.Context;
using CExtension.Permission;
using CExtension.Swagger;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace CAPI
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // 参考:https://github.com/autofac/Examples/blob/master/src/AspNetCore3Example/Startup.cs
        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Add any Autofac modules or registrations.
            // This is called AFTER ConfigureServices so things you
            // register here OVERRIDE things registered in ConfigureServices.
            //
            // You must have the call to `UseServiceProviderFactory(new AutofacServiceProviderFactory())`
            // when building the host or this won't be called.
            builder.RegisterModule(new AutofacModule());
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMediatR(typeof(Startup));

            services.ContextServiceSetup();

            services.ConfigureSwaggerService();

            services.ConfigureAuthService();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            //ConfigureAuthService(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.ConfigureSwagger();

            // 认证
            app.UseAuthentication();

            // 授权
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureAuthService(IServiceCollection services)
        {
            var identityUrl = "https://localhost:5001";

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //    //options.DefaultChallengeScheme = nameof(ResponseHandler);
            //    //options.DefaultForbidScheme = nameof(ResponseHandler);
            //}).AddJwtBearer(options =>
            //{
            //    options.Authority = identityUrl;
            //    options.RequireHttpsMetadata = false;
            //    options.Audience = "api1";
            //});
            //.AddScheme<AuthenticationSchemeOptions, ResponseHandler>(nameof(ResponseHandler), o => { });

            // 这里冗余写了一次,因为很多人看不到
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            //第一种认证写法：就算成功登录，请求接口还是会返回401
            //services.AddAuthentication(o =>
            //{
            //    o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            //    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    o.DefaultChallengeScheme = nameof(ResponseHandler);
            //    o.DefaultForbidScheme = nameof(ResponseHandler);
            //})
            //.AddJwtBearer(options =>
            //{
            //    options.Authority = "https://localhost:5001";
            //    options.RequireHttpsMetadata = false;
            //    options.Audience = "api1";
            //})
            //.AddScheme<AuthenticationSchemeOptions, ResponseHandler>(nameof(ResponseHandler), o => { });

            //services
            //    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            //    {
            //        options.Authority = "https://localhost:5001";
            //        options.RequireHttpsMetadata = false;
            //        //options.Audience = "CAPI";
            //        options.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false };
            //    })
            //    .AddScheme<AuthenticationSchemeOptions, ResponseHandler>(nameof(ResponseHandler), o => { });

            //第一种认证写法：成功登录就可以请求接口
            services
                .AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = identityUrl;
                    options.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false };
                })
                .AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>(nameof(AuthenticationHandler), o => { });

            //授权
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    //policy.RequireAuthenticatedUser();
                    //policy.RequireClaim("scope", "api1");
                    policy.RequireScope("api");
                });
            });

            // 注入权限处理器
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();

            #region 参数
            //读取配置文件
            var symmetricKeyAsBase64 = "secret";
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var Issuer = "API";
            var Audience = "wr";

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // 如果要数据库动态绑定，这里先留个空，后边处理器里动态赋值
            var permission = new List<PermissionItem>();

            // 角色与接口的权限要求参数
            var permissionRequirement = new PermissionRequirement(
                "/api/denied",// 拒绝授权的跳转地址（目前无用）
                permission,
                ClaimTypes.Role,//基于角色的授权
                Issuer,//发行人
                Audience,//听众
                signingCredentials,//签名凭据
                expiration: TimeSpan.FromSeconds(60 * 60)//接口的过期时间
                );
            #endregion


            //// 3、自定义复杂的策略授权
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("permission",
            //             policy => policy.Requirements.Add(permissionRequirement));
            //});


            //// 4、基于Scope策略授权
            //services.AddAuthorization(options =>
            //{
            //    options.AddPolicy("Scope_BlogModule_Policy", builder =>
            //    {
            //        //客户端Scope中包含blog.core.api.BlogModule才能访问
            //        builder.RequireScope("blog.core.api.BlogModule");
            //    });

            //    // 其他 Scope 策略
            //    // ...

            //});

            //services.AddSingleton(permissionRequirement);
        }
    }

    //public static class CustomConfig
    //{
    //    public static void ConfigureSwaggerService(this IServiceCollection services)
    //    {

    //    }
    //}
}
