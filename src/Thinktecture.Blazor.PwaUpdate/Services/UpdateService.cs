using Microsoft.JSInterop;

namespace Thinktecture.Blazor.PwaUpdate.Services
{
    public class UpdateService : IUpdateService, IAsyncDisposable
    {
        private readonly Lazy<ValueTask<IJSInProcessObjectReference>> _moduleTask;

        public Action UpdateAvailable { get; set; }
        public UpdateService(IJSRuntime js)
            => _moduleTask = new(() => js.InvokeAsync<IJSInProcessObjectReference>(
                "import", "./_content/Thinktecture.Blazor.PwaUpdate/Thinktecture.Blazor.PwaUpdate.js"));

        public async Task InitializeServiceWorkerUpdateAsync()
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("registerUpdateEvent", DotNetObjectReference.Create(this), nameof(OnUpdateAvailable));
        }

        public async Task ReloadAsync()
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("reload");
        }

        [JSInvokable(nameof(OnUpdateAvailable))]
        public void OnUpdateAvailable()
        {
            UpdateAvailable?.Invoke();
        }

        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}
