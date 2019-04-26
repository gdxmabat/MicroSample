using Koa.Integration.PubSub;
using Koa.Integration.PubSub.DependencyInjection;
using Koa.Integration.PubSub.Azure.ServiceBus;
using Koa.Integration.PubSub.Azure.ServiceBus.DependencyInjection;
using MicroSample.Business.Event.User;
using Microsoft.AspNetCore.Builder;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Koa.Serialization.Json;

namespace MicroSample.Api.Configuration.ServiceBusQueue
{
  internal static class ServiceBusQueueConfig
  {
    public static void ConfigureServices(IServiceCollection services, ServiceBusQueueConfiguration config)
    {
      AddServiceBusEventHandlers(services, config);
    }

    public static void Configure(IApplicationBuilder app)
    {
      ConfigureEventBus(app);
    }

    private static void AddServiceBusEventHandlers(IServiceCollection services, ServiceBusQueueConfiguration configuration)
    {
      services.TryAddSingleton<IJsonSerializer, JsonSerializer>();
      services.AddKoaPubSub();
      services.AddAzureServiceBusPubSub();
      services.AddEventBus<AzureServiceBusEventBus>();


      services.AddQueue(sp =>
            {
              var connectionString = configuration.ConnectionString;
              var entityPath = configuration.EntityPath;
              var queueClient = new QueueClient(connectionString, entityPath);

              return queueClient;
            });
      
      services.AddEventHandler<UserCreatedEventHandler, UserCreatedEvent>();
    }

    private static void ConfigureEventBus(IApplicationBuilder app)
    {
      app.ApplicationServices.InitializeReceivers();
      app.ApplicationServices.InitializeHandlers();

    }
  }
}