using IntegrationTest.Api.Configuration;
using IntegrationTest.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IntegrationTest.Api.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddConfiguration(this IServiceCollection services)
        {
            services.AddMvc().AddApplicationPart(typeof(IServiceCollectionExtension).Assembly).AddNewtonsoftJson();
        }

        public static void AddBDConfiguration(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddDbContext<AplicationDbContext>(options =>
             options.UseSqlServer(appSettings.ConnectionString,
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(Startup).Assembly.FullName);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                }));
        }

    }
}
