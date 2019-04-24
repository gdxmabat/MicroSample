using Xunit;

namespace MicroSample.Business.Test
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