using IntegrationTest.Api.Configuration;
using IntegrationTest.Api.Data;
using IntegrationTest.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace IntegrationTest.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var appSettings = GetAppSettingsConfig();
            services.Configure<AppSettings>(Configuration);
            services.AddTransient<AplicationDbContextSeed>();

            services.AddBDConfiguration(appSettings);

            services.AddConfiguration();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Integration Test API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
             {
                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "Integration Test V1");
                 c.RoutePrefix = string.Empty;
             });
            app.AddRoutingConfiguration();
        }

        private AppSettings GetAppSettingsConfig()
        {
            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);

            return appSettings;
        }

    }
}
