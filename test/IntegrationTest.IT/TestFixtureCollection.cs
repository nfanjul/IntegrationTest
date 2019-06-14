using Xunit;

namespace IntegrationTest.IT
{
    // ITEST 4
    [CollectionDefinition("Test")]
    public class TestFixtureCollection : ICollectionFixture<TestServerFixture> { }
}
