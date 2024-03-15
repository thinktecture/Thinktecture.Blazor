using Microsoft.Extensions.DependencyInjection;
using PatrickJahr.Blazor.PwaUpdate.Services;

namespace PatrickJahr.Blazor.PwaUpdate
{
    public static class PwaUpdateServiceCollectionExtensions
    {
        public static IServiceCollection AddUpdateService(this IServiceCollection services)
        {
            return services.AddScoped<IUpdateService, UpdateService>();
        }
    }
}
