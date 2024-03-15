using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Thinktecture.Blazor.AsyncClipboard.Models;

/// <summary>
/// Defines the appropriate presentation for the content pasted.
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverterWithAttributeSupport))]
public enum PresentationStyle
{
    /// <summary>
    /// The presentation style was not specified.
    /// </summary>
    [EnumMember(Value = "unspecified")]
    Unspecified,
    /// <summary>
    /// Contents should be added inline.
    /// </summary>
    [EnumMember(Value = "inline")]
    Inline,
    /// <summary>
    /// Contents should be treated as an attachment.
    /// </summary>
    [EnumMember(Value = "attachment")]
    Attachment,
}
