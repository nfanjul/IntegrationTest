using IntegrationTest.Api.Data;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace IntegrationTest.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // SHOW 3
            CreateWebHostBuilder(args).Build().BuildContext().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
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
