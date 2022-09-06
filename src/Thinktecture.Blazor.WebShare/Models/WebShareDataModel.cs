using System.Text.Json.Serialization;
using Microsoft.JSInterop;

namespace Thinktecture.Blazor.WebShare.Models
{
    public class WebShareDataModel
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Title { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Text { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Url { get; set; }
        
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IJSObjectReference[]? Files { get; set; }
    }
}
