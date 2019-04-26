using System.Net.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace MicroSample.Api.Test
{
    public class CompositionRootFixture
    {
        private readonly TestServer server;

        public HttpClient Client { get; }

        public CompositionRootFixture()
        {

            this.server = new TestServer(new WebHostBuilder().UseStartup<Startup>());
            this.Client = this.server.CreateClient();
        }


    }
}
