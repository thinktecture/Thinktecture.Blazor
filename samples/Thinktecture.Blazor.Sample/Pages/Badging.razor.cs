using Microsoft.AspNetCore.Components;
using Thinktecture.Blazor.Badging;

namespace Thinktecture.Blazor.Sample.Pages
{
    public partial class Badging
    {
        [Inject] private BadgingService _badgingService { get; set; } = default!;
        
        private bool _isBadgingSupported;

        protected override async void OnInitialized()
        {
            _isBadgingSupported = await _badgingService.IsSupportedAsync();
            await InvokeAsync(StateHasChanged);
            await base.OnInitializedAsync();
        }

        private async void SetAppBadge(int? content)
        {
            await _badgingService.SetAppBadgeAsync(content);
        }

        private async void ClearAppBadge()
        {
            await _badgingService.ClearAppBadgeAsync();
        }
    }
}