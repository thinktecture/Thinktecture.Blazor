using Microsoft.JSInterop;

namespace Thinktecture.Blazor.PwaUpdate.Services
{
    public class UpdateService : JSModule
    {
        public Action UpdateAvailable { get; set; }
        public UpdateService(IJSRuntime js)
            : base(js, "./_content/Thinktecture.Blazor.PwaUpdate/Thinktecture.Blazor.PwaUpdate.js")
        {
        }

        public async Task InitializeServiceWorkerUpdate()
        {
            await InvokeVoidAsync("registerUpdateEvent", DotNetObjectReference.Create(this), nameof(OnUpdateAvailable));
        }

        [JSInvokable(nameof(OnUpdateAvailable))]
        public void OnUpdateAvailable()
        {
            UpdateAvailable?.Invoke();
        }
    }
}
