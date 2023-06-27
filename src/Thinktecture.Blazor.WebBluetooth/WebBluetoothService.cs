using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.JSInterop;
using Thinktecture.Blazor.WebBluetooth.Models;

namespace Thinktecture.Blazor.WebBluetooth;

public class WebBluetoothService : IWebBluetoothService, IAsyncDisposable
{
    private readonly Lazy<ValueTask<IJSInProcessObjectReference>> _moduleTask;

    public WebBluetoothService(IJSRuntime jsRuntime)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSInProcessObjectReference>(
            "import", "./_content/Thinktecture.Blazor.WebBluetooth/Thinktecture.Blazor.WebBluetooth.js"));
    }
    
    /// <summary>
    /// Determines if the Web Bluetooth API is supported on the target user agent.
    /// </summary>
    /// <returns>A boolean value indicating if the Web Bluetooth API is supported.</returns>
    public async Task<bool> IsSupportedAsync()
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<bool>("isSupported");
    }

    public async Task<BluetoothDevice?> GetDeviceAsync(string name)
    {
        try
        {
            var module = await _moduleTask.Value;
            var jsObject = await module.InvokeAsync<IJSObjectReference>("getDevice", name);
            var jsName = await module.InvokeAsync<string>("getName", jsObject);
            var jsId = await module.InvokeAsync<string>("getId", jsObject);
            var jsConnected = await module.InvokeAsync<bool>("getConnected", jsObject);
            return BluetoothDevice.CreateAsync(jsObject, jsName, jsId, jsConnected);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return null;
        }
    }

    public async ValueTask DisposeAsync()
    {
        if (_moduleTask.IsValueCreated)
        {
            var module = await _moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}