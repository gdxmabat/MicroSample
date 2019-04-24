using System.Linq;
using MicroSample.Business.Model;
using MicroSample.Business.Repository;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MicroSample.Business.Test.Post
{
    public class PostRepositoryTest : BaseTest
    {
        private readonly IPostRepository repository;

        public PostRepositoryTest(CompositionRootFixture fixture): base(fixture)
        {
            this.repository = fixture.ServiceProvider.GetService<IPostRepository>();
        }

        [Fact]
        public void CanInjectTest()
        {
            Assert.NotNull(this.repository);
        }

        [Fact]
        public void GetAllTest()
        {
            var models = this.repository.GetAll<PostModel>();
            Assert.NotNull(models);
            Assert.Single(models);
        }
    }
}
