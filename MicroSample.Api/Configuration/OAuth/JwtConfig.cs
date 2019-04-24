using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace MicroSample.Api.Configuration.OAuth
{
    internal static class JwtConfig
    {
        public static void ConfigureServices(IServiceCollection services, AuthConfiguration authConfiguration)
        {
            if (authConfiguration?.IsEnabled == true)
            {
                services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options => {
                        
                        options.Authority = authConfiguration.Authority;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidIssuers = new []
                            {
                                authConfiguration.Authority
                            },
                            ValidAudiences = new[]
                            {
                                authConfiguration.Authority + "/resources", authConfiguration.Application
                            },
                        };
                        //options.BackchannelHttpHandler = new HttpClientHandler()
                        //{
                        //    Proxy = new WebProxy(proxyuri, true, new string[0], new NetworkCredential(userName, password, domain))
                        //};
                    });
            }
        }
        public static void Configure(IApplicationBuilder app, AuthConfiguration authConfiguration)
        {

            if (authConfiguration?.IsEnabled == true)
            {
                app.UseAuthentication();
            }
        }

        private static Task AuthenticationFailed(AuthenticationFailedContext arg)
        {
            // For debugging purposes only!
            var s = $"AuthenticationFailed: {arg.Exception.Message}";
            arg.Response.ContentLength = s.Length;
            arg.Response.Body.Write(Encoding.UTF8.GetBytes(s), 0, s.Length);
            return Task.FromResult(0);
        }
    }
}