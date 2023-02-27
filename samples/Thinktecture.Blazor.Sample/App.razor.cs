using Microsoft.AspNetCore.Components;
using Thinktecture.Blazor.Notification.Services;

namespace Thinktecture.Blazor.Sample {
    public partial class App
    {
        [Inject] private NotificationService _notificationService { get; set; } = default!;

        protected override async Task OnInitializedAsync() {
            await _notificationService.InitializeServiceWorkerNotificationAsync();
            await base.OnInitializedAsync();
        }
    }
}