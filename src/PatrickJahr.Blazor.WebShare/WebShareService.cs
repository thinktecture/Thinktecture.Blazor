using Microsoft.JSInterop;
using PatrickJahr.Blazor.WebShare.Models;

namespace PatrickJahr.Blazor.WebShare
{
    public class WebShareService : IAsyncDisposable
    {
        private readonly Lazy<ValueTask<IJSInProcessObjectReference>> _moduleTask;

        public WebShareService(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSInProcessObjectReference>(
                "import", "./_content/PatrickJahr.Blazor.WebShare/PatrickJahr.Blazor.WebShare.js"));
        }

        /// <summary>
        /// Determines if the Web Share API is supported on the target user agent.
        /// </summary>
        /// <returns>A boolean value indicating if the Web Share API is supported.</returns>
        public async Task<bool> IsSupportedAsync()
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<bool>("isSupported");
        }

        /// <summary>
        /// Determines if the data can be shared via the current user agent.
        /// </summary>
        /// <param name="data">The data that is supposed to be shared.</param>
        /// <returns>A boolean value indicating if the data can be shared.</returns>
        /// <exception cref="Exception">
        /// Throws an exception if the canShare() method is not available on the target platform.
        /// </exception>
        public async ValueTask<bool> CanShareAsync(WebShareDataModel data)
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<bool>("canShare", data);
        }

        /// <summary>
        /// Shares the given data via the platform-specific sharing mechanism. The task completes if the data was
        /// successfully shared with another application.
        /// </summary>
        /// <param name="data">The data to share.</param>
        /// <exception cref="Exception">
        /// Throws an exception if the share() method is not available on the target platform, the data cannot be shared
        /// or the user dismisses the share operation.
        /// </exception>
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