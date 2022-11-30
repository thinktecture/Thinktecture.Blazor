using KristofferStrube.Blazor.FileSystemAccess;

namespace Thinktecture.Blazor.FileHandling.Models;

public class LaunchParams
{
    // TODO: The files are actually a read-only list of FileSystemHandles (see
    // https://wicg.github.io/web-app-launch/#launchparams-interface), but converting the types currently is hard and
    // most of the time, users will open files only.
    /// <summary>
    /// List of <see cref="FileSystemFileHandle"/>s the application was opened with.
    /// </summary>
    public IReadOnlyList<FileSystemFileHandle> Files { get; }
    
    /// <summary>
    /// Represents the URL of launch.
    /// </summary>
    public string? TargetUrl { get; }

    internal LaunchParams(IReadOnlyList<FileSystemFileHandle> files, string? targetUrl)
    {
        Files = files;
        TargetUrl = targetUrl;
    }
}