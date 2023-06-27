using Microsoft.AspNetCore.Components;
using Thinktecture.Blazor.ScreenWakeLock;

namespace Thinktecture.Blazor.Sample.Pages;

public partial class ScreenWakeLock
{
    [Inject] private IScreenWakeLockService _screenWakeLockService { get; set; } = default!;

    private bool _wakeLockRequested;
    private bool _isSupported;

    protected override async Task OnInitializedAsync()
    {
        _isSupported = await _screenWakeLockService.IsSupportedAsync();
        _screenWakeLockService.WakeLockReleased = () =>
        {
            _wakeLockRequested = false;
        };
        await base.OnInitializedAsync();
    }

    private async Task ToggleScreenWakeLock()
    {
        if (_wakeLockRequested)
        {
            await _screenWakeLockService.ReleaseWakeLockAsync();
        }
        else
        {
            try
            {
                await _screenWakeLockService.RequestWakeLockAsync();
                _wakeLockRequested = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Request screen wake lock failed. Error: {e.Message}");
                _wakeLockRequested = false;
            }
        }
    }
}