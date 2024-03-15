using Microsoft.Extensions.DependencyInjection;

namespace Thinktecture.Blazor.AsyncClipboard
{
    public static class AsyncClipboardServiceCollectionExtensions
    {
        public static IServiceCollection AddAsyncClipboardService(this IServiceCollection services)
        {
            return services.AddScoped<AsyncClipboardService>();
        }
    }
}