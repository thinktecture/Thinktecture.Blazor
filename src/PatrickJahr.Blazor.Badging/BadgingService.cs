using Microsoft.JSInterop;

namespace PatrickJahr.Blazor.Badging
{
    public class BadgingService : IAsyncDisposable
    {
        private readonly Lazy<ValueTask<IJSInProcessObjectReference>> _moduleTask;

        public BadgingService(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSInProcessObjectReference>(
                "import", "./_content/PatrickJahr.Blazor.Badging/PatrickJahr.Blazor.Badging.js"));
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

        /// <summary>
        /// Sets a badge on the current app's icon. If a value is passed to this method, it will be set as the value of
        /// the badge. Otherwise, a generic badge will be shown, as defined by the platform.
        /// </summary>
        /// <param name="contents">The value of the badge.</param>
        /// <exception cref="Exception">Throws an exception if the action is not supported.</exception>
        public async ValueTask SetAppBadgeAsync(int? contents = null)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("setAppBadge", contents);
        }

        /// <summary>
        /// Clears the badge on the current app's icon.
        /// </summary>
        /// <exception cref="Exception">Throws an exception if the action is not supported.</exception>
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