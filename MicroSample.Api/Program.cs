using System.IO;
using MicroSample.Api.Configuration.KeyVault;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace MicroSample.Api
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args)
    {
      return WebHost.CreateDefaultBuilder(args)
          .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                  var env = hostingContext.HostingEnvironment;
                  config
                      .SetBasePath(env.ContentRootPath)
                      .AddJsonFile("appsettings.json", true, true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", true, true)
                      .AddJsonFile("secrets/appsettings.secret", true, true)
                      .AddEnvironmentVariables();
                  if (env.IsDevelopment())
                  {
                    config.AddUserSecrets<Startup>();
                  }

                  config = KeyVaultConfig.ConfigureEnvironment(config);

                })
                .ConfigureLogging((hostingContext, logging) =>
                {
                  logging
                      .AddConfiguration(hostingContext.Configuration.GetSection("Logging"))
                      .AddConsole()
                      .AddDebug();
                });

    }
  }
}
