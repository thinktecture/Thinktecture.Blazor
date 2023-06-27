using Microsoft.AspNetCore.Components;
using Thinktecture.Blazor.WebBluetooth;
using Thinktecture.Blazor.WebBluetooth.Models;

namespace Thinktecture.Blazor.Sample.Pages;

public partial class WebBluetooth
{
    [Inject] private IWebBluetoothService _bluetoothService { get; set; } = default!;
    
    private bool _isSupported;
    private bool _requestDevice;
    private BluetoothDevice? _device;
    
    protected override async Task OnInitializedAsync()
    {
        _isSupported = await _bluetoothService.IsSupportedAsync();
        await base.OnInitializedAsync();
    }

    private async Task GetDevice()
    {
        _requestDevice = true;
        _device = await _bluetoothService.GetDeviceAsync("TT Trackpad");
        _requestDevice = false;
    }
}