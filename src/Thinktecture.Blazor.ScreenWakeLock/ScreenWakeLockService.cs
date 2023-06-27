using Microsoft.JSInterop;

namespace Thinktecture.Blazor.ScreenWakeLock;

public class ScreenWakeLockService: IScreenWakeLockService, IAsyncDisposable
{
    private readonly Lazy<ValueTask<IJSInProcessObjectReference>> _moduleTask;
    private DotNetObjectReference<ScreenWakeLockService> _dotNetObjectReference;

    public Action WakeLockReleased { get; set; }
    
    public ScreenWakeLockService(IJSRuntime jsRuntime)
    {
        _moduleTask = new(() => jsRuntime.InvokeAsync<IJSInProcessObjectReference>(
            "import", "./_content/Thinktecture.Blazor.ScreenWakeLock/Thinktecture.Blazor.ScreenWakeLock.js"));
        _dotNetObjectReference = DotNetObjectReference.Create(this);
    }
    
    public async Task<bool> IsSupportedAsync()
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<bool>("isSupported");
    }

    public async Task RequestWakeLockAsync()
    {
        var module = await _moduleTask.Value;
        try
        {
            await module.InvokeVoidAsync("requestWakeLock", _dotNetObjectReference, nameof(OnWakeLockReleased));
        }
        catch(Exception e)
        {
            throw new JSException(e.Message);
        }
    }
    
    public async Task ReleaseWakeLockAsync()
    {
        var module = await _moduleTask.Value;
        await module.InvokeVoidAsync("releaseWakeLock");
    }

    [JSInvokable]
    public void OnWakeLockReleased()
    {
        WakeLockReleased?.Invoke();
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