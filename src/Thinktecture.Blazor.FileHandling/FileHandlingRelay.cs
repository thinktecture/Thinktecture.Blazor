using KristofferStrube.Blazor.FileSystemAccess;
using Microsoft.JSInterop;
using Thinktecture.Blazor.FileHandling.Models;

namespace Thinktecture.Blazor.FileHandling;

public class FileHandlingRelay
{
    private readonly IJSRuntime _jsRuntime;
    private readonly Lazy<ValueTask<IJSInProcessObjectReference>> _moduleTask;

    public FileHandlingRelay(IJSRuntime jsRuntime, Lazy<ValueTask<IJSInProcessObjectReference>> moduleTask)
    {
        _jsRuntime = jsRuntime;
        _moduleTask = moduleTask;
    }
    
    [JSInvokable]
    public async ValueTask InvokeConsumer(DotNetObjectReference<Action<LaunchParams>> consumerRef)
    {
        var module = await _moduleTask.Value;
        // Workaround for https://github.com/dotnet/aspnetcore/issues/26049
        // When this is fixed, pass targetURL and list of files to this method directly.
        var targetUrl = await module.InvokeAsync<string?>("getTargetUrl");
        var files = await module.InvokeAsync<IJSObjectReference>("getFiles");
        var filesLength = await module.InvokeAsync<int>("getArrayLength", files);
        var filesList = new List<FileSystemFileHandle>();

        for (var i = 0; i < filesLength; i++)
        {
            var fileRef = await files.InvokeAsync<IJSObjectReference>("at", i);
            filesList.Add(FileSystemFileHandle.Create(_jsRuntime, fileRef));
        }

        consumerRef.Value.Invoke(new LaunchParams(filesList, targetUrl));
        consumerRef.Dispose();
    }
}