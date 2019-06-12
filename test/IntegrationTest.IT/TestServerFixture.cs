using IntegrationTest.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;

namespace IntegrationTest.IT
{
    public class TestServerFixture
    {
        public TestServer Server { get; private set; }
        private IConfigurationRoot Configuration;

        public TestServerFixture()
        {
            Server = CreateServer();
        }

        public TestServer CreateServer()
        {
            Configuration = new TestConfigurationBuilder().Build();
            var builder = new WebHostBuilder().UseStartup<TestStartup>();
            builder.UseConfiguration(Configuration);
            
            var server = new TestServer(builder);
            server.Host.BuildContext();
            return server;
        }

    }
}
