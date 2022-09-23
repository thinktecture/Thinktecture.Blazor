using System.Text.Json.Serialization;

namespace Thinktecture.Blazor.AsyncClipboard.Models;

/// <summary>
/// Additional metadata for this <see cref="ClipboardItem"/>.
/// </summary>
public class ClipboardItemOptions
{
    /// <summary>
    /// Defines the appropriate presentation for the content pasted.
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public PresentationStyle PresentationStyle { get; set; }
}