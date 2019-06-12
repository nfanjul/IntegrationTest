using System.Reflection;
using Xunit.Sdk;

namespace IntegrationTest.IT.Attributes
{
    public class ResetAttribute : BeforeAfterTestAttribute
    {
        private readonly string _connectionString;
        public ResetAttribute()
        {
            var configuration = new TestConfigurationBuilder().Build();
            _connectionString = configuration["ConnectionString"];
        }

        public override void Before(MethodInfo methodUnderTest)
        {
            DbFixture.Reset(_connectionString);
        }
    }

}
