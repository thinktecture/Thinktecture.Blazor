using Microsoft.AspNetCore.Components;
using Thinktecture.Blazor.ScreenWakeLock;

namespace Thinktecture.Blazor.Sample.Pages;

public partial class ScreenWakeLock
{
    [Inject] private IScreenWakeLockService _screenWakeLockService { get; set; } = default!;

    private bool _enteredFullScreen = false;
    private bool _wakeLockRequested = false;
    private bool _isSupported = false;

    protected override async Task OnInitializedAsync()
    {
        _isSupported = await _screenWakeLockService.IsSupportedAsync();
        await base.OnInitializedAsync();
    }

    private async Task ToggleFullScreen()
    {
        if (_enteredFullScreen)
        {
            await _screenWakeLockService.ExitFullScreenAsync();
        }
        else
        {
            await _screenWakeLockService.EnterFullScreenAsync();
        }

        _enteredFullScreen = !_enteredFullScreen;
    }

    private async Task ToggleScreenWakeLock()
    {
        if (_wakeLockRequested)
        {
            await _screenWakeLockService.ReleaseWakeLockAsync();
        }
        else
        {
            await _screenWakeLockService.RequestWakeLockAsync();
        }
    }
}