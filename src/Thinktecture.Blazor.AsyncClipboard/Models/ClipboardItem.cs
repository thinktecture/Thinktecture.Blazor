using System.Collections.Immutable;
using Microsoft.JSInterop;

namespace Thinktecture.Blazor.AsyncClipboard.Models;

public class ClipboardItem
{
    private readonly IJSObjectReference? _jsObjectReference;

    // TODO: OneOf
    internal readonly Dictionary<string, IJSObjectReference>? Items;
    internal readonly ClipboardItemOptions? Options;

    public ClipboardItem(Dictionary<string, IJSObjectReference> items, ClipboardItemOptions? options = null)
    {
        Items = items;
        Options = options;
    }

    internal ClipboardItem(IJSObjectReference jsObjectReference)
    {
        _jsObjectReference = jsObjectReference;
    }

    // TODO: Serialization!
    public PresentationStyle PresentationStyle => Options?.PresentationStyle ?? PresentationStyle.Unspecified;
    public long LastModified { get; set; } // TODO
    public IReadOnlyCollection<string> Types => Items.Keys.ToImmutableArray();

    public bool Delayed { get; set; } // TODO

    public ValueTask<IJSObjectReference> GetType(string type)
    {
        if (Items != null)
        {
            // If ClipboardItem was created by Blazor
            return ValueTask.FromResult(Items[type]);
        }

        if (_jsObjectReference != null)
        {
            // If ClipboardItem was created by JS
            return _jsObjectReference.InvokeAsync<IJSObjectReference>("getType", type);
        }

        throw new Exception("Invalid object state.");
    }
    
    // createDelayed()
}
