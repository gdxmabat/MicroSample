using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Koa.Platform.Providers.DependencyInjection;
using Koa.Mapping.ObjectMapper.Automapper.DependencyInjection;
using Koa.Persistence.EntityRepository.EntityFrameworkCore.DependencyInjection;
using MicroSample.Business.Repository;
using MicroSample.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MicroSample.Business.Test
{
    public class CompositionRootFixture
    {
        protected readonly IServiceCollection Services;
        public IServiceProvider ServiceProvider { get; }
        public IConfigurationRoot Configuration { get; }

        public CompositionRootFixture()
        {
            var builder = new ConfigurationBuilder();
            this.Configuration = builder.Build();
            this.Services = new ServiceCollection();
            this.ConfigureServices();
            this.ServiceProvider = this.Services.BuildServiceProvider();
            this.Configure();
        }



        private void ConfigureServices()
        {
            // Add Koa services
            var dbContextType = typeof(DataContext);
            var domainAssembly = dbContextType.GetTypeInfo().Assembly;
            var repositoryType = typeof(IPostRepository);
            var bussinessAssembly = repositoryType.GetTypeInfo().Assembly;

            this.Services.AddDateTimeProvider();
            this.Services.AddIdentityProvider();
            this.Services.UseKoaEntityFramework<DataContext>(bussinessAssembly, opt => opt.UseInMemoryDatabase("Default"));
            this.Services.UseKoaAutomaper(bussinessAssembly);
            this.Services.AddTransient(x => this.ServiceProvider);
        }

        private void Configure()
        {
            var context = this.ServiceProvider.GetService<DataContext>();
            context.Seed();
        }
    }
}
