using IntegrationTest.Api.Configuration;
using IntegrationTest.Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;

namespace IntegrationTest.Api
{
    public class Startup
    {
        // SHOW 1
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

            // SHOW 2
            services.AddDbContext<AplicationDbContext>(options =>
             options.UseSqlServer(appSettings.ConnectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Startup).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                }));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Integration Test API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseMvc();
        }

        private AppSettings GetAppSettingsConfig()
        {
            var appSettings = new AppSettings();
            Configuration.Bind(appSettings);

            return appSettings;
        }
    }
}
