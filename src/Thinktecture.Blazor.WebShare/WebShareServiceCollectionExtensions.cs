using Microsoft.Extensions.DependencyInjection;

namespace Thinktecture.Blazor.WebShare
{
    public static class WebShareServiceCollectionExtensions
    {
        public static IServiceCollection AddWebShareService(this IServiceCollection services)
        {
            return services.AddScoped<WebShareService>();
        }
    }
}
