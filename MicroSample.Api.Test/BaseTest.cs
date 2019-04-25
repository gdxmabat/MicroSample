using Xunit;

namespace MicroSample.Api.Test
{
    public class BaseTest : IClassFixture<CompositionRootFixture>
    {
        protected readonly CompositionRootFixture Fixture;

        public BaseTest(CompositionRootFixture fixture)
        {
            this.Fixture = fixture;
        }
    }
}