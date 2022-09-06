using Microsoft.Extensions.DependencyInjection;

namespace Thinktecture.Blazor.WebShare
{
    public static class WebShareServiceCollectionExtensions
    {
        public static IServiceCollection AddWebShareServices(this IServiceCollection services)
        {
            return services.AddScoped<WebShareService>();
        }
    }
}
