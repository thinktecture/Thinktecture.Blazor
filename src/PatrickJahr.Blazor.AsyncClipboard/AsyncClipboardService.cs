using Microsoft.JSInterop;
using PatrickJahr.Blazor.AsyncClipboard.Models;

namespace PatrickJahr.Blazor.AsyncClipboard
{
    public class AsyncClipboardService : IAsyncDisposable
    {
        private readonly ValueTask<IJSObjectReference> _moduleTask;

        public AsyncClipboardService(IJSRuntime jsRuntime)
        {
            // This moduleTask must not be lazy, as all clipboard methods need to be executed during user activation
            // (i.e., a key press or click). Loading the module on demand leads to an asynchronous operation and loss of
            // user activation in Safari, so the first attempt of using one of the APIs would always fail.
            _moduleTask = jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/PatrickJahr.Blazor.AsyncClipboard/PatrickJahr.Blazor.AsyncClipboard.js");
        }

        /// <summary>
        /// Determines if the Async Clipboard API is supported on the target user agent.
        /// </summary>
        /// <returns>A boolean value indicating if the Async Clipboard API is supported.</returns>
        public async ValueTask<bool> IsSupportedAsync()
        {
            var module = await _moduleTask;
            return await module.InvokeAsync<bool>("isSupported");
        }

        /// <summary>
        /// Writes plain text to the clipboard.
        /// </summary>
        /// <param name="data">The string to write.</param>
        public async ValueTask WriteTextAsync(string data)
        {
            var module = await _moduleTask;
            await module.InvokeVoidAsync("writeText", data);
        }

        /// <summary>
        /// Writes clipboard items to the clipboard. Please note that this method must be the first asynchronous call
        /// in your event handler. If you need to perform asynchronous work, pass in a promise for the according media
        /// type within the clipboard item. Use the <see cref="GetObjectReference"/> helper method to synchronously
        /// get an object reference, e.g., to a promise.
        /// </summary>
        /// <param name="data">The list of clipboard items.</param>
        public async ValueTask WriteAsync(IEnumerable<ClipboardItem> data)
        {
            var module = await _moduleTask;
            var jsClipboardItemArray = await GetJsClipboardItemsAsync(data);
            await module.InvokeVoidAsync("write", jsClipboardItemArray);
        }

        private async ValueTask<IJSObjectReference> GetJsClipboardItemsAsync(IEnumerable<ClipboardItem> clipboardItems)
        {
            var module = await _moduleTask;
            return await module.InvokeAsync<IJSObjectReference>("getClipboardItems", clipboardItems.Select(item => new
            {
                item.Items,
                item.Options
            }));
        }

        /// <summary>
        /// Reads plain text from the clipboard.
        /// </summary>
        /// <returns>The clipboard contents in plain text representation.</returns>
        public async ValueTask<string> ReadTextAsync()
        {
            var module = await _moduleTask;
            return await module.InvokeAsync<string>("readText");
        }

        /// <summary>
        /// Reads clipboard items from the clipboard.
        /// </summary>
        /// <returns>A list of clipboard items.</returns>
        public async ValueTask<IEnumerable<ClipboardItem>> ReadAsync()
        {
            var module = await _moduleTask;
            var jsClipboardItemArray = await module.InvokeAsync<IJSObjectReference>("read");
            var clipboardItemObjectReferences = await GetArrayObjectReferencesAsync(jsClipboardItemArray);
            var clipboardItems = new List<ClipboardItem>();
            foreach (var clipboardItemObjectReference in clipboardItemObjectReferences)
            {
                clipboardItems.Add(await ClipboardItem.CreateAsync(clipboardItemObjectReference, module));
            }

            return clipboardItems;
        }

        private async Task<IEnumerable<IJSObjectReference>> GetArrayObjectReferencesAsync(
            IJSObjectReference arrayObjectReference)
        {
            var module = await _moduleTask;
            var count = await module.InvokeAsync<int>("getArrayLength", arrayObjectReference);
            var list = new List<IJSObjectReference>();
            for (var i = 0; i < count; i++)
            {
                list.Add(await arrayObjectReference.InvokeAsync<IJSObjectReference>("at", i));
            }

            return list;
        }

        /// <summary>
        /// A helper method for synchronously retrieving an <see cref="IJSObjectReference"/>.
        /// </summary>
        /// <param name="module">The module to invoke.</param>
        /// <param name="identifier">The method name to invoke.</param>
        /// <param name="args">Arguments to pass to the method.</param>
        /// <returns>An <see cref="IJSObjectReference"/> to the value returned from the method.</returns>
        /// <exception cref="ArgumentException">
        /// Throws an exception in case the module cannot be cast to an <see cref="IJSInProcessObjectReference"/>.
        /// </exception>
        public IJSObjectReference GetObjectReference(IJSObjectReference module, string identifier,
            params object[]? args)
        {
            if (module is not IJSInProcessObjectReference inProcessObjectReference)
            {
                throw new ArgumentException("Given module cannot be cast to IJSInProcessObjectReference.",
                    nameof(module));
            }

            return inProcessObjectReference.Invoke<IJSObjectReference>(identifier, args);
        }

        public async ValueTask DisposeAsync()
        {
            await (await _moduleTask).DisposeAsync();
        }
    }
}