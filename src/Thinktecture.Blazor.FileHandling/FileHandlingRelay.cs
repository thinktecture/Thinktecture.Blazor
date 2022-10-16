using Microsoft.JSInterop;
using Thinktecture.Blazor.FileHandling.Models;

namespace Thinktecture.Blazor.FileHandling;

public class FileHandlingRelay
{
    private readonly Lazy<ValueTask<IJSInProcessObjectReference>> _moduleTask;

    public FileHandlingRelay(Lazy<ValueTask<IJSInProcessObjectReference>> moduleTask)
    {
        _moduleTask = moduleTask;
    }
    
    [JSInvokable]
    public async ValueTask InvokeConsumer(DotNetObjectReference<Action<LaunchParams>> consumerRef)
    {
        var module = await _moduleTask.Value;
        // Workaround for https://github.com/dotnet/aspnetcore/issues/26049
        // When this is fixed, pass list of files to this method directly.
        var files = await module.InvokeAsync<IJSObjectReference>("getFiles");
        var filesLength = await module.InvokeAsync<int>("getArrayLength", files);
        var filesList = new List<IJSObjectReference>();

        for (var i = 0; i < filesLength; i++)
        {
            var fileRef = await files.InvokeAsync<IJSObjectReference>("at", i);
            filesList.Add(fileRef);
        }

        consumerRef.Value.Invoke(new LaunchParams(filesList));
        consumerRef.Dispose();
    }
}