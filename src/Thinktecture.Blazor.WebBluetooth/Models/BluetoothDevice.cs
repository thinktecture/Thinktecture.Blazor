using Microsoft.JSInterop;

namespace Thinktecture.Blazor.WebBluetooth.Models;

public class BluetoothDevice: IAsyncDisposable
{
    private readonly IJSObjectReference _reference;

    public string Id { get; set; }
    public string Name { get; set; }
    public bool Connected { get; set; }
    
    private BluetoothDevice(IJSObjectReference reference)
    {
        _reference = reference;
    }

    internal static BluetoothDevice CreateAsync(IJSObjectReference jsObjectReference, string name, string id, bool connected)
    {
        var result = new BluetoothDevice(jsObjectReference)
        {

            Name = name,
            Id = id,
            Connected = connected
        };
        return result;
    }

    public async Task ConnectAsync()
    {
        await _reference.InvokeVoidAsync("connect");
    }
    
    public async Task DisconnectAsync()
    {
        await _reference.InvokeVoidAsync("disconnect");
    }

    public async ValueTask DisposeAsync()
    {
        await _reference.DisposeAsync();
    }
}