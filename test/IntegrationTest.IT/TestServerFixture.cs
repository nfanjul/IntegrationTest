using IntegrationTest.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace IntegrationTest.IT
{
    public class TestServerFixture
    {
        // ITEST 1
        public TestServer Server { get; private set; }
        private IConfigurationRoot Configuration;

        public TestServerFixture()
        {
            Server = CreateServer();
        }

        public TestServer CreateServer()
        {
            // ITEST 2
            Configuration = new TestConfigurationBuilder().Build();
            // ITEST 3
            var builder = new WebHostBuilder().UseStartup<TestStartup>();
            builder.UseConfiguration(Configuration);
            
            var server = new TestServer(builder);
            server.Host.BuildContext();
            return server;
        }

    }
}
