using Microsoft.Extensions.DependencyInjection;

namespace Thinktecture.Blazor.FileHandling
{
    public static class FileHandlingServiceCollectionExtensions
    {
        public static IServiceCollection AddFileHandlingService(this IServiceCollection services)
        {
            return services.AddScoped<FileHandlingService>();
        }
    }
}