using System.Text.Json.Serialization;
using Microsoft.JSInterop;

namespace PatrickJahr.Blazor.WebShare.Models
{
    public class WebShareDataModel
    {
        /// <summary>
        /// A string representing a title to be shared.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; set; }

        /// <summary>
        /// A string representing a text to be shared.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Text { get; set; }

        /// <summary>
        /// A string representing a URL to be shared.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Url { get; set; }

        /// <summary>
        /// An array of <see cref="IJSObjectReference" />s pointing to JavaScript File objects to be shared.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IJSObjectReference[]? Files { get; set; }
    }
}