using Microsoft.JSInterop;

namespace Thinktecture.Blazor.WebShare.Models
{
    public class WebShareDataModel
    {
        public string Title { get; set; } = string.Empty;
        public string Text { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public IJSObjectReference[]? Files { get; set; }
    }
}
