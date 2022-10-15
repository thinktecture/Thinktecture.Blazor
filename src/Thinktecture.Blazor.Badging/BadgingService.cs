using Microsoft.JSInterop;

namespace Thinktecture.Blazor.Badging
{
    public class BadgingService : IAsyncDisposable
    {
        private readonly Lazy<ValueTask<IJSInProcessObjectReference>> _moduleTask;

        public BadgingService(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSInProcessObjectReference>(
                "import", "./_content/Thinktecture.Blazor.Badging/Thinktecture.Blazor.Badging.js"));
        }

        /// <summary>
        /// Determines if the Badging API is supported on the target user agent.
        /// </summary>
        /// <returns>A boolean value indicating if the Badging API is supported.</returns>
        public async Task<bool> IsSupportedAsync()
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<bool>("isSupported");
        }
        
        // TODO: Comments
        // TODO: Readme

        /// <summary>
        /// Determines if the data can be shared via the current user agent.
        /// </summary>
        /// <param name="contents">The data that is supposed to be shared.</param>
        /// <returns>A boolean value indicating if the data can be shared.</returns>
        /// <exception cref="Exception">
        /// Throws an exception if the canShare() method is not available on the target platform.
        /// </exception>
        public async ValueTask SetAppBadgeAsync(int? contents = null)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("setAppBadge", contents);
        }

        /// <summary>
        /// 
        /// </summary>
        public async ValueTask ClearAppBadgeAsync()
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("clearAppBadge");
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