using IntegrationTest.Api.Configuration;
using IntegrationTest.Api.Data;
using IntegrationTest.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTest.IT
{
    public class TestStartup
    {
        public IConfiguration Configuration { get; }

        public TestStartup(IConfiguration config)
        {
            Configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = GetAppSettingsConfig();
            services.Configure<AppSettings>(Configuration);
            services.AddTransient<AplicationDbContextSeed>();

            services.AddBDConfiguration(appSettings);
            services.AddConfiguration();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.AddRoutingConfiguration();
        }

        public AppSettings GetAppSettingsConfig()
        {
            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);

            return appSettings;
        }
    }
}
