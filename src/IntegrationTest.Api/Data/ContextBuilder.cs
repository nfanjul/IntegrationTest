using IntegrationTest.Api.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTest.Api.Data
{
    public static class ContextBuilder
    {
        public static IWebHost BuildContext(this IWebHost builder)
        {
            // SHOW 4
            builder
                .MigrateDbContext<AplicationDbContext>((context, services) =>
                {
                    var applicationDbContextSeed = services.GetService<AplicationDbContextSeed>();
                    applicationDbContextSeed.EnsureSeedAsync().Wait();
                });

            return builder;
        }

    }
}