using Microsoft.Extensions.DependencyInjection;

namespace Thinktecture.Blazor.ImageHelper
{
    public static class ImageHelperServiceExtensions
    {
        public static IServiceCollection AddImageHelperService(this IServiceCollection services)
        {
            return services.AddScoped<ImageHelperService>();
        }
    }
}
