using KristofferStrube.Blazor.FileSystem;
using Microsoft.JSInterop;
using PatrickJahr.Blazor.FileHandling.Models;

namespace PatrickJahr.Blazor.FileHandling;

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
        var filesList = new List<FileSystemHandle>();

        for (var i = 0; i < filesLength; i++)
        {
            var fileRef = await files.InvokeAsync<IJSObjectReference>("at", i);
            var fileSystemHandle = await GetFileSystemHandleAsync(fileRef);
            filesList.Add(fileSystemHandle);
        }

        consumerRef.Value.Invoke(new LaunchParams(filesList, targetUrl));
        consumerRef.Dispose();
    }

    private async Task<FileSystemHandle> GetFileSystemHandleAsync(IJSObjectReference fileRef)
    {
        var fileHandle = FileSystemHandle.Create(_jsRuntime, fileRef);
        var fileHandleKind = await fileHandle.GetKindAsync();
        await fileHandle.DisposeAsync();
        
        if (fileHandleKind == FileSystemHandleKind.Directory)
        {
            return FileSystemDirectoryHandle.Create(_jsRuntime, fileRef);
        }
        
        if (fileHandleKind == FileSystemHandleKind.File)
        {
            return FileSystemFileHandle.Create(_jsRuntime, fileRef);
        }

        throw new InvalidOperationException("Unsupported file system handle kind.");
    }
}