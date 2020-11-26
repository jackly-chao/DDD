using CCommon.Enums;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CExtension.Swagger
{
    public static class Swagger
    {
        public static void ConfigureSwaggerService(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddSwaggerGen(c =>
            {
                typeof(ApiVersionEnum).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new OpenApiInfo
                    {
                        Title = $"CAPI文档--{RuntimeInformation.FrameworkDescription}",
                        Version = version,
                        Description = $"CAPI " + version,
                        Contact = new OpenApiContact
                        {
                            Name = "Jackly",
                            Email = "1815427244@qq.com"
                        }
                    });
                });

                // 开启加权小锁
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // 在header中添加token，传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();

                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        Implicit = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri($"https://localhost:5001/connect/authorize"),
                            Scopes = new Dictionary<string, string>
                            {
                                { "testApi","TestApi" }
                            }
                        }
                    }
                });

                //为 Swagger JSON and UI设置xml文档注释路径
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath, true);

                var viewModelXmlPath = Path.Combine(AppContext.BaseDirectory, "CApplication.xml");
                c.IncludeXmlComments(xmlPath, true);
            });
        }

        public static void ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger()
                .UseSwaggerUI(c =>
                {
                    typeof(ApiVersionEnum).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                    {
                        c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"CAPI文档 {version}");
                        c.RoutePrefix = string.Empty;
                        c.OAuthClientId("testClient");
                    });
                });
        }
    }
}
