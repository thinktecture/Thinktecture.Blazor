using Microsoft.Extensions.DependencyInjection;
using Thinktecture.Blazor.PwaUpdate.Services;

namespace Thinktecture.Blazor.PwaUpdate
{
    public static class PwaUpdateServiceCollectionExtensions
    {
        public static IServiceCollection AddUpdateService(this IServiceCollection services)
        {
            return services.AddScoped<IUpdateService, UpdateService>();
        }
    }
}
