using Microsoft.Extensions.DependencyInjection;

namespace Thinktecture.Blazor.WebBluetooth;

public static class WebBluetoothServiceCollectionExtensions
{
    public static void AddWebBluetoothService(this IServiceCollection services)
    {
        services.AddScoped<IWebBluetoothService, WebBluetoothService>();
    }
}