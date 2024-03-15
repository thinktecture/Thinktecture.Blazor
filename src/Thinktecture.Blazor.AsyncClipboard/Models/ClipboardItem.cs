using System.Collections.Immutable;
using Microsoft.JSInterop;

namespace Thinktecture.Blazor.AsyncClipboard.Models;

public class ClipboardItem
{
    private readonly IJSObjectReference? _jsObjectReference;
    private readonly IReadOnlyList<string>? _types;

    // TODO: OneOf
    internal readonly ImmutableDictionary<string, IJSObjectReference>? Items;
    internal readonly ClipboardItemOptions Options;

    /// <summary>
    /// Defines the appropriate presentation for the content pasted.
    /// </summary>
    public PresentationStyle PresentationStyle => Options.PresentationStyle;

    /// <summary>
    /// Constructs a new clipboard item.
    /// </summary>
    /// <param name="items">
    /// Maps media types to <see cref="IJSObjectReference"/>s that must either point to a JavaScript string or Blob.
    /// </param>
    /// <param name="options">Optional <see cref="ClipboardItemOptions"/>.</param>
    public ClipboardItem(Dictionary<string, IJSObjectReference> items, ClipboardItemOptions? options = null)
    {
        Items = items.ToImmutableDictionary();
        Options = options ?? new ClipboardItemOptions {PresentationStyle = PresentationStyle.Unspecified};
    }

    /// <summary>
    /// Internal constructor to construct the C# object from a JavaScript clipboard item.
    /// </summary>
    /// <param name="jsObjectReference">The <see cref="IJSObjectReference"/> pointing to the clipboard item.</param>
    /// <param name="types">The read-only list of media types.</param>
    /// <param name="options">The clipboard item options.</param>
    private ClipboardItem(IJSObjectReference jsObjectReference, IReadOnlyList<string> types,
        ClipboardItemOptions options)
    {
        _jsObjectReference = jsObjectReference;
        _types = types;
        Options = options;
    }

    internal static async ValueTask<ClipboardItem> CreateAsync(IJSObjectReference jsObjectReference,
        IJSObjectReference moduleReference)
    {
        var types = await moduleReference.InvokeAsync<IEnumerable<string>>("getClipboardItemTypes", jsObjectReference);
        var options =
            await moduleReference.InvokeAsync<ClipboardItemOptions>("getClipboardItemOptions", jsObjectReference);
        return new ClipboardItem(jsObjectReference, types.ToImmutableArray(), options);
    }

    /// <summary>
    /// The list of media types contained in this clipboard item.
    /// </summary>
    /// <exception cref="Exception">Throws an exception in case the list could not be retrieved.</exception>
    public IReadOnlyCollection<string> Types
    {
        get
        {
            if (Items is not null)
            {
                return Items.Keys.ToImmutableList();
            }

            if (_types is not null)
            {
                return _types.ToImmutableList();
            }

            throw new Exception("Invalid object state.");
        }
    }

    /// <summary>
    /// Returns a ValueTask with a Blob corresponding to the given media type.
    /// </summary>
    /// <param name="type">The media type.</param>
    /// <returns>A ValueTask with a Blob corresponding to the given media type.</returns>
    /// <exception cref="Exception">
    /// Throws an exception in case a Blob corresponding to the media type could not be retrieved.
    /// </exception>
    public ValueTask<IJSObjectReference> GetTypeAsync(string type)
    {
        if (Items is not null)
        {
            // If ClipboardItem was created by Blazor
            return ValueTask.FromResult(Items[type]);
        }

        if (_jsObjectReference is not null)
        {
            // If ClipboardItem was created by JS
            return _jsObjectReference.InvokeAsync<IJSObjectReference>("getType", type);
        }

        throw new Exception("Invalid object state.");
    }
}