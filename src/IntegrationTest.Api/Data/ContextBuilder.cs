using IntegrationTest.Api.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IntegrationTest.Api.Data
{
    public static class ContextBuilder
    {
        public static IHost BuildContext(this IHost host)
        {
            host
                .MigrateDbContext<AplicationDbContext>((context, services) =>
                {
                    var applicationDbContextSeed = services.GetService<AplicationDbContextSeed>();
                    applicationDbContextSeed.EnsureSeedAsync().Wait();
                });

            return host;
        }

    }
}