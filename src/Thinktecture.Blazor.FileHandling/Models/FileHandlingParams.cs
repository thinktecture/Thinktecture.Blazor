using Microsoft.JSInterop;

namespace Thinktecture.Blazor.FileHandling.Models;

public class LaunchParams
{
    public IReadOnlyList<IJSObjectReference> Files { get; }

    internal LaunchParams(IReadOnlyList<IJSObjectReference> files)
    {
        Files = files;
    }
}