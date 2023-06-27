using Thinktecture.Blazor.WebBluetooth.Models;

namespace Thinktecture.Blazor.WebBluetooth;

public interface IWebBluetoothService
{
    /// <summary>
    /// Determines if the Web Bluetooth API is supported on the target user agent.
    /// </summary>
    /// <returns>A boolean value indicating if the Web Bluetooth API is supported.</returns>
    Task<bool> IsSupportedAsync();

    Task<BluetoothDevice?> GetDeviceAsync(string name);
}