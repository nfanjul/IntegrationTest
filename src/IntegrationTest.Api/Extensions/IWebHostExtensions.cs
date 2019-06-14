using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Data.SqlClient;

namespace IntegrationTest.Api.Extensions
{
    public static class IWebHostExtensions
    {
        // SHOW 5
        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using (var scope = webHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var logger = services.GetRequiredService<ILogger<TContext>>();
                var context = services.GetService<TContext>();
                logger.LogInformation($"Migrating database associated with context {typeof(TContext).Name}");

                var retry = Policy.Handle<SqlException>()
                     .WaitAndRetry(new TimeSpan[]
                     {
                             TimeSpan.FromSeconds(3),
                             TimeSpan.FromSeconds(5),
                             TimeSpan.FromSeconds(8),
                     });

                retry.Execute(() =>
                {
                    context.Database
                    .Migrate();

                    seeder(context, services);
                });

                logger.LogInformation($"Migrated database associated with context {typeof(TContext).Name}");
            }

            return webHost;
        }
    }
}