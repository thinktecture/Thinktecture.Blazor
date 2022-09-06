using Microsoft.AspNetCore.Components;
using Thinktecture.Blazor.WebShare;

namespace Thinktecture.Blazor.Sample.Pages
{
    public partial class Index
    {
        [Inject] private WebShareService _webShareService { get; set; }

        private bool _isWebShareSupported = false;

        protected override async Task OnInitializedAsync()
        {
            _isWebShareSupported = await _webShareService.IsSupportedAsync();
            await base.OnInitializedAsync();
        }
    }
}