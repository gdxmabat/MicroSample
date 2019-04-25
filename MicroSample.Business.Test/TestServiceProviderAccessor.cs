using System;

namespace MicroSample.Business.Test
{
    public class TestServiceProviderAccessor
    {
        public TestServiceProviderAccessor(IServiceProvider services)
        {
            this.Services = services;
        }

        public IServiceProvider Services { get; }
    }
}