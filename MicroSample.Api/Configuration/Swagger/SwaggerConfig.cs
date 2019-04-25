using System.Collections.Generic;
using System.IO;
using MicroSample.Api.Configuration.OAuth;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace MicroSample.Api.Configuration.Swagger
{
    internal static class SwaggerConfig
    {
        public static void ConfigureServices(IServiceCollection services, SwaggerConfiguration swaggerConfiguration, AuthConfiguration authConfiguration)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Version = "v1",
                        Title = "Swashbuckle Sample API",
                        Description = "A sample API for testing Swashbuckle",
                        TermsOfService = "Some terms ..."
                    }
                );
                if (authConfiguration?.IsEnabled == true)
                {
                    c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                    {
                        Type = "oauth2",
                        Flow = "implicit",
                        AuthorizationUrl = $"{authConfiguration.Authority}/connect/authorize",
                        Scopes = new Dictionary<string, string>
                        {
                            //{"openid","openid"},
                            {$"{authConfiguration.Scope}",authConfiguration.Scope}
                        }
                    });
                    c.OperationFilter<SecurityRequirementsOperationFilter>();
                }

            });
            services.ConfigureSwaggerGen(c =>
            {
                c.IncludeXmlComments(GetXmlCommentsPath(PlatformServices.Default.Application));
            });
        }

        public static void Configure(IApplicationBuilder app, SwaggerConfiguration swaggerConfiguration, AuthConfiguration authConfiguration)
        {
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) => swagger.Host = httpReq.Host.Value);

            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "V1 Docs");

                if (authConfiguration?.IsEnabled == true)
                {
                    c.OAuthClientId(swaggerConfiguration.ClientId);                    
                    c.OAuthRealm(authConfiguration.Application);
                    c.OAuthAppName(swaggerConfiguration.ApplicationName);
                    c.OAuthScopeSeparator(" ");
                }

            });
        }

        private static string GetXmlCommentsPath(ApplicationEnvironment appEnvironment)
        {
            return Path.Combine(appEnvironment.ApplicationBasePath, "MicroSample.Api.xml");
        }


    }
}
