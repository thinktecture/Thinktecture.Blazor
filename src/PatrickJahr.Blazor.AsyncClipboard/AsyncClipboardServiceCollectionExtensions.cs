using Microsoft.Extensions.DependencyInjection;

namespace PatrickJahr.Blazor.AsyncClipboard
{
    public static class AsyncClipboardServiceCollectionExtensions
    {
        public static IServiceCollection AddAsyncClipboardService(this IServiceCollection services)
        {
            return services.AddScoped<AsyncClipboardService>();
        }
    }
}