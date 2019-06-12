using Xunit;

namespace IntegrationTest.IT
{
    [CollectionDefinition("Test")]
    public class TestFixtureCollection : ICollectionFixture<TestServerFixture>
    {

    }
}
