using System.IO;
using System.Net;
using System.Reflection;
using Koa.Hosting.AspNetCore.DependencyInjection;
using Koa.Hosting.AspNetCore.ModelBinding;
using Koa.Mapping.ObjectMapper.Automapper.DependencyInjection;
using Koa.Persistence.EntityRepository.EntityFrameworkCore.DependencyInjection;
using Koa.Platform.Providers.DependencyInjection;
using MicroSample.Api.Configuration.OAuth;
using MicroSample.Api.Configuration.ServiceBusQueue;
using MicroSample.Api.Configuration.Swagger;
using MicroSample.Business;
using MicroSample.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MicroSample.Api
{
  public class Startup
  {
    public Startup(IHostingEnvironment env, IConfiguration config)
    {

      this.Configuration = config;
      this.AuthConfiguration = this.Configuration.GetSection(AuthConfiguration.Section).Get<AuthConfiguration>();
      this.SwaggerConfiguration = this.Configuration.GetSection(SwaggerConfiguration.Section).Get<SwaggerConfiguration>();
      this.ServiceBusQueueConfiguration = config.GetSection(ServiceBusQueueConfiguration.Section).Get<ServiceBusQueueConfiguration>();

    }

    private IConfiguration Configuration { get; }
    private AuthConfiguration AuthConfiguration { get; }
    private SwaggerConfiguration SwaggerConfiguration { get; }
    private ServiceBusQueueConfiguration ServiceBusQueueConfiguration { get; }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      JwtConfig.ConfigureServices(services, this.AuthConfiguration);
      // Add framework services.
      services.AddMvc(config =>
      {
        config.ModelBinderProviders.Insert(0, new WebApiPagedModelBinderProvider());

        if (this.AuthConfiguration?.IsEnabled == true)
        {
          var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
          config.Filters.Add(new AuthorizeFilter(policy));
        }
      });
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddSingleton<IConfiguration>(this.Configuration);

      // Add CORS services
      services.AddCors();

      // Add Koa services
      var businessPointerTipe = typeof(BusinessPointer);
      var bussinessAssembly = businessPointerTipe.GetTypeInfo().Assembly;

      services.AddDateTimeProvider();
      services.AddIdentityProvider();
      services.UseKoaEntityFramework<DataContext>(bussinessAssembly, opt => opt.UseInMemoryDatabase("DefaultDatabase"));
      services.UseKoaAutomaper(bussinessAssembly);
      services.AddHttpContextDependencies();


      // Add Swagger services
      SwaggerConfig.ConfigureServices(services, this.SwaggerConfiguration, this.AuthConfiguration);

      //Add ServiceBusQueue services
      if (this.ServiceBusQueueConfiguration?.IsEnabled == true)
      {
        ServiceBusQueueConfig.ConfigureServices(services, this.ServiceBusQueueConfiguration);
      }
      
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {

      app.UseExceptionHandler(new ExceptionHandlerOptions()
      {
        ExceptionHandler = async context =>
        {
          context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

          var ex = context.Features.Get<IExceptionHandlerFeature>()?.Error;
          if (ex == null) return;

          var error = new
          {
            message = ex.Message,
            detail = ex.StackTrace
          };

          context.Response.ContentType = "application/json";

          using (var writer = new StreamWriter(context.Response.Body))
          {
            new JsonSerializer().Serialize(writer, error);
            await writer.FlushAsync().ConfigureAwait(false);
          }
        }
      });


      using (var scope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
      {
        var provider = scope.ServiceProvider;
        using (var dbContext = provider.GetRequiredService<DataContext>())
        {
          dbContext.Seed();
        }
      }
      

      app.UseCors(builder => builder.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod().AllowCredentials());

      JwtConfig.Configure(app, this.AuthConfiguration);

      app.UseStaticFiles();
      app.UseMvc();

      //Configure Swagger
      SwaggerConfig.Configure(app, this.SwaggerConfiguration, this.AuthConfiguration);

      //Configure ServiceBusQueue
      if (this.ServiceBusQueueConfiguration?.IsEnabled == true)
      {
        ServiceBusQueueConfig.Configure(app);
      }
      
    }
  }
}