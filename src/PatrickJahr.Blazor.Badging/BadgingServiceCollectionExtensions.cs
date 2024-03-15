using Microsoft.Extensions.DependencyInjection;

namespace PatrickJahr.Blazor.Badging
{
    public static class BadgingServiceCollectionExtensions
    {
        public static IServiceCollection AddBadgingService(this IServiceCollection services)
        {
            return services.AddScoped<BadgingService>();
        }
    }
}