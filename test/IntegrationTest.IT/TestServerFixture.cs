using IntegrationTest.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace IntegrationTest.IT
{
    public class TestServerFixture
    {
        // SHOW 1
        public TestServer Server { get; private set; }
        public AplicationDbContext AplicationDbContext { get; private set; }

        private readonly IConfiguration _configuration;

        public TestServerFixture()
        {
            _configuration = new TestConfigurationBuilder().Build();
            // SHOW 2 -->
            Server = CreateServer();
            AplicationDbContext = GetAplicationDbContext();
        }

        public AplicationDbContext GetAplicationDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AplicationDbContext>();
            optionsBuilder.UseSqlServer(_configuration["ConnectionString"], setup =>
            {
                setup.MigrationsAssembly(typeof(AplicationDbContext).Assembly.FullName);
            });

            return new AplicationDbContext(optionsBuilder.Options);
        }

        public TestServer CreateServer()
        {
            var host = new HostBuilder()
                .ConfigureWebHost(webBuilder =>
                {
                    webBuilder
                        .UseTestServer()
                        .ConfigureAppConfiguration((context, builder) =>
                        {
                            builder.AddConfiguration(_configuration);
                        })
                        .UseStartup<TestStartup>();
                }).Start();

            host.BuildContext();

            return host.GetTestServer();
        }

    }
}
