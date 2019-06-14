using IntegrationTest.Api;
using IntegrationTest.Api.Configuration;
using IntegrationTest.Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;

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
            services.AddTransient<AplicationDbContextSeed>();
            services.AddDbContext<AplicationDbContext>(options =>
             options.UseSqlServer(appSettings.ConnectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Startup).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                }));

            services.AddMvcCore()
                .AddJsonFormatters()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                    options.SerializerSettings.DefaultValueHandling = DefaultValueHandling.Ignore;
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        public AppSettings GetAppSettingsConfig()
        {
            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);

            return appSettings;
        }
    }
}
