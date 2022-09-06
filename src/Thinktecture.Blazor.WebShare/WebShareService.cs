using Microsoft.JSInterop;
using Thinktecture.Blazor.WebShare.Models;

namespace Thinktecture.Blazor.WebShare
{
    public class WebShareService : IAsyncDisposable
    {
        private readonly Lazy<ValueTask<IJSInProcessObjectReference>> _moduleTask;
        private readonly IJSRuntime _jsRuntime;

        public WebShareService(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSInProcessObjectReference>(
                "import", "./_content/Thinktecture.Blazor.WebShare/Thinktecture.Blazor.WebShare.js"));
            _jsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
        }

        public async Task<bool> IsSupportedAsync(bool basicSupport = false)
        {
            return basicSupport
                ? await _jsRuntime.InvokeAsync<bool>("navigator.hasOwnProperty", "share")
                : await _jsRuntime.InvokeAsync<bool>("navigator.hasOwnProperty", "share") 
                    && await _jsRuntime.InvokeAsync<bool>("navigator.hasOwnProperty", "canShare");
        }

        public async ValueTask<bool> CanShareAsync(WebShareDataModel data)
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<bool>("canShare", data);
        }

        public async Task ShareAsync(WebShareDataModel data)
        {
            var module = await _moduleTask.Value;
            await module.InvokeAsync<bool>("share", data);
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
