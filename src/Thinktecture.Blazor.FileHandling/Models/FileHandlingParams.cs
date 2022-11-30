using KristofferStrube.Blazor.FileSystemAccess;

namespace Thinktecture.Blazor.FileHandling.Models;

public class LaunchParams
{
    // The files are actually a read-only list of FileSystemHandles (see
    // https://wicg.github.io/web-app-launch/#launchparams-interface), but converting the types currently is hard and
    // most of the time, users will open files only.
    public IReadOnlyList<FileSystemFileHandle> Files { get; }
    
    public string? TargetUrl { get; }

    internal LaunchParams(IReadOnlyList<FileSystemFileHandle> files, string? targetUrl)
    {
        Files = files;
        TargetUrl = targetUrl;
    }
}