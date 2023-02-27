using Microsoft.AspNetCore.Components;
using Thinktecture.Blazor.Notification.Services;

namespace Thinktecture.Blazor.Sample.Pages {
    public partial class Notification
    {
        [Inject] private NotificationService _notificationService { get; set; } = default!;

        private async Task ShowNotification() 
        {
            await _notificationService.ShowNotification("Test", "Test Nachricht");
        }
    }
}