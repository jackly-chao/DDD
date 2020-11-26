using CExtension.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace CExtension.Authentication
{
    /// <summary>
    /// 认证服务
    /// </summary>
    public static class AuthenticationService
    {
        public static void ConfigureAuthService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            #region Ids4
            var identityUrl = "https://localhost:5001";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = nameof(AuthenticationHandler);
                options.DefaultForbidScheme = nameof(AuthenticationHandler);
            }).AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                //options.Audience = "testApi";
                options.TokenValidationParameters = new TokenValidationParameters { ValidateAudience = false };
            }).AddScheme<AuthenticationSchemeOptions, AuthenticationHandler>(nameof(AuthenticationHandler), o => { });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //授权
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireScope("testApi");
                });
            });

            services.AddScoped<IAuthorizationHandler, AuthorizationHandler>();
            #endregion

        }
    }
}
