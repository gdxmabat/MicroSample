using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Koa.Hosting.AspNetCore.Model;
using MicroSample.Business.Model;
using Newtonsoft.Json;
using Xunit;

namespace MicroSample.Api.Test
{
    public class PostControllerTest : BaseTest
    {
        public PostControllerTest(CompositionRootFixture fixture) : base(fixture)
        {
        }

        [Fact]
        public async Task Get()
        {
            // Act
            var response = await this.Fixture.Client.GetAsync("/api/Post");
            response.EnsureSuccessStatusCode();

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            Assert.False(string.IsNullOrWhiteSpace(content));

            var model = JsonConvert.DeserializeObject<EnvelopedModel<PostModel>>(content);
            Assert.NotNull(model);
        }
    }
}
