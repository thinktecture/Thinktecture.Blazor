using Microsoft.JSInterop;

namespace Thinktecture.Blazor.Notification.Services {
    public class NotificationService : JSModule {
        public Action<string> ActionClicked { get; set; }

        public NotificationService(IJSRuntime js) 
            : base(js, "./_content/Thinktecture.Blazor.Notification/Thinktecture.Blazor.Notification.js") {
        }

        public async Task InitializeServiceWorkerNotificationAsync() {
            await InvokeVoidAsync("registerNavigationClick", DotNetObjectReference.Create(this), nameof(OnNotificationClicked));
        }

        public async Task ShowNotification(string title, string message) {
            await InvokeVoidAsync("showNotification", title, message);
        }

        [JSInvokable(nameof(OnNotificationClicked))]
        public void OnNotificationClicked(string action) {
            ActionClicked?.Invoke(action);
        }
    }
}
