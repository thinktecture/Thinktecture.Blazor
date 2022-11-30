using Microsoft.JSInterop;
using Thinktecture.Blazor.FileHandling.Models;

namespace Thinktecture.Blazor.FileHandling
{
    public class FileHandlingService : IAsyncDisposable
    {
        private readonly Lazy<ValueTask<IJSInProcessObjectReference>> _moduleTask;
        private readonly DotNetObjectReference<FileHandlingRelay> _relayReference;

        public FileHandlingService(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSInProcessObjectReference>(
                "import", "./_content/Thinktecture.Blazor.FileHandling/Thinktecture.Blazor.FileHandling.js"));
            _relayReference = DotNetObjectReference.Create(new FileHandlingRelay(jsRuntime, _moduleTask));
        }

        /// <summary>
        /// Determines if the File Handling API is supported on the target user agent.
        /// </summary>
        /// <returns>A boolean value indicating if the File Handling API is supported.</returns>
        public async Task<bool> IsSupportedAsync()
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<bool>("isSupported");
        }
        
        /// <summary>
        /// Sets a consumer function to get access to the <see cref="LaunchParams"/>.
        /// </summary>
        /// <param name="consumer">The launch consumer.</param>
        /// <exception cref="Exception">Throws an exception if the action is not supported.</exception>
        public async ValueTask SetConsumerAsync(Action<LaunchParams> consumer)
        {
            var consumerReference = DotNetObjectReference.Create(consumer);
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("setConsumer", _relayReference, consumerReference);
        }

        public async ValueTask DisposeAsync()
        {
            if (_moduleTask.IsValueCreated)
            {
                var module = await _moduleTask.Value;
                await module.DisposeAsync();
            }
            
            _relayReference.Dispose();
        }
    }
}