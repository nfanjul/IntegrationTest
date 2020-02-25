using Xunit;

namespace IntegrationTest.IT
{
    // SHOW 3
    [CollectionDefinition("Test")]
    public class TestFixtureCollection : ICollectionFixture<TestServerFixture> { }
}
