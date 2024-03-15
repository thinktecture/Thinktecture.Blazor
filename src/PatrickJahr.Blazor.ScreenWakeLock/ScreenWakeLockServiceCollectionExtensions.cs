using Microsoft.Extensions.DependencyInjection;

namespace PatrickJahr.Blazor.ScreenWakeLock;

public static class ScreenWakeLockServiceCollectionExtensions
{
    public static void AddScreenWakeLockService(this IServiceCollection services)
    {
        services.AddScoped<IScreenWakeLockService, ScreenWakeLockService>();
    }
}