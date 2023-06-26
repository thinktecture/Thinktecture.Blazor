using Microsoft.JSInterop;

namespace Thinktecture.Blazor.ScreenWakeLock;

public class ScreenWakeLockService: IScreenWakeLockService
{
    private readonly IJSRuntime _jsRuntime;
    private readonly Lazy<ValueTask<IJSInProcessObjectReference>> _moduleTask;

    public ScreenWakeLockService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSInProcessObjectReference>(
            "import", "./_content/Thinktecture.Blazor.ScreenWakeLock/Thinktecture.Blazor.ScreenWakeLock.js"));
    }
    
    public async Task<bool> IsSupportedAsync()
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<bool>("isSupported");
    }

    public async Task EnterFullScreenAsync()
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("enterFullScreen");
    }

    public async Task ExitFullScreenAsync()
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("exitFullScreen");
    }

    public async Task RequestWakeLockAsync()
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("requestWakeLock");
    }
    
    public async Task ReleaseWakeLockAsync()
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("releaseWakeLock");
    }
}