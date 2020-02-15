using IntegrationTest.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace IntegrationTest.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // SHOW 3
            CreateHostBuilder(args).Build().BuildContext().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureAppConfiguration((context, configurationBuilder) =>
                    {
                        var environmentName = context.HostingEnvironment.EnvironmentName;
                        configurationBuilder
                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                            .AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables();
                    });
    }
}
