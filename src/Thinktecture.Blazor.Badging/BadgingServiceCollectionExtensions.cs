using Microsoft.Extensions.DependencyInjection;

namespace Thinktecture.Blazor.Badging
{
    public static class BadgingServiceCollectionExtensions
    {
        public static IServiceCollection AddBadgingService(this IServiceCollection services)
        {
            return services.AddScoped<BadgingService>();
        }
    }
}