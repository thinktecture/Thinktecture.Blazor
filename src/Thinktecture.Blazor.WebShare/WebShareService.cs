using Microsoft.JSInterop;
using Thinktecture.Blazor.WebShare.Models;

namespace Thinktecture.Blazor.WebShare
{
    public class WebShareService : IAsyncDisposable
    {
        private readonly Lazy<ValueTask<IJSInProcessObjectReference>> _moduleTask;

        public WebShareService(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSInProcessObjectReference>(
                "import", "./_content/Thinktecture.Blazor.WebShare/Thinktecture.Blazor.WebShare.js"));
        }

        public async Task<bool> IsSupportedAsync()
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<bool>("isSupported");
        }

        public async ValueTask<bool> CanShareAsync(WebShareDataModel data)
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<bool>("canShare", data);
        }

        public async Task ShareAsync(WebShareDataModel data)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("share", data);
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
