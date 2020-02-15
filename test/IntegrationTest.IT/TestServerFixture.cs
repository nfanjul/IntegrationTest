﻿using IntegrationTest.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace IntegrationTest.IT
{
    public class TestServerFixture
    {
        // ITEST 1
        public TestServer Server { get; private set; }
        public AplicationDbContext AplicationDbContext { get; private set; }

        private readonly IConfiguration _configuration;

        public TestServerFixture()
        {
            // ITEST 2
            _configuration = new TestConfigurationBuilder().Build();
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
            // ITEST 3
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
