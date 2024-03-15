using Microsoft.Extensions.DependencyInjection;

namespace PatrickJahr.Blazor.FileHandling
{
    public static class FileHandlingServiceCollectionExtensions
    {
        public static IServiceCollection AddFileHandlingService(this IServiceCollection services)
        {
            return services.AddScoped<FileHandlingService>();
        }
    }
}