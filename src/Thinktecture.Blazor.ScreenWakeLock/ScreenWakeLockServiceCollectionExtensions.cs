using Microsoft.Extensions.DependencyInjection;

namespace Thinktecture.Blazor.ScreenWakeLock;

public static class ScreenWakeLockServiceCollectionExtensions
{
    public static void AddScreenWakeLockService(this IServiceCollection services)
    {
        services.AddScoped<IScreenWakeLockService, ScreenWakeLockService>();
    }
}