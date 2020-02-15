using Microsoft.AspNetCore.Builder;

namespace IntegrationTest.Api.Extensions
{
    public static class IApplicationBuilderExtension
    {
        public static void AddRoutingConfiguration(this IApplicationBuilder app)
        {
            app.UseRouting()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapDefaultControllerRoute();
                });
        }
    }
}
