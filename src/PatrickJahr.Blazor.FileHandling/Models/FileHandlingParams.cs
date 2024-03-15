using KristofferStrube.Blazor.FileSystem;

namespace PatrickJahr.Blazor.FileHandling.Models;

public class LaunchParams
{
    /// <summary>
    /// List of <see cref="FileSystemFileHandle"/>s the application was opened with.
    /// </summary>
    public IReadOnlyList<FileSystemHandle> Files { get; }
    
    /// <summary>
    /// Represents the URL of launch.
    /// </summary>
    public string? TargetUrl { get; }

    internal LaunchParams(IReadOnlyList<FileSystemHandle> files, string? targetUrl)
    {
        Files = files;
        TargetUrl = targetUrl;
    }
}