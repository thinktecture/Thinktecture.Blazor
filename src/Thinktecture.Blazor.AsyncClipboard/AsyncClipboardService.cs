using Microsoft.JSInterop;
using Thinktecture.Blazor.AsyncClipboard.Models;

namespace Thinktecture.Blazor.AsyncClipboard
{
    public class AsyncClipboardService : IAsyncDisposable
    {
        private readonly ValueTask<IJSObjectReference> moduleTask;

        public AsyncClipboardService(IJSRuntime jsRuntime)
        {
            // This moduleTask must not be lazy, as all clipboard methods need to be executed during user activation
            // (i.e., a key press or click). Loading the module on demand leads to an asynchronous operation and loss of
            // user activation in Safari, so the first attempt of using one of the APIs would always fail.
            moduleTask = jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/Thinktecture.Blazor.AsyncClipboard/Thinktecture.Blazor.AsyncClipboard.js");
        }

        public async ValueTask WriteText(string data)
        {
            var module = await moduleTask;
            await module.InvokeVoidAsync("writeText", data);
        }

        public async ValueTask Write(IEnumerable<ClipboardItem> data)
        {
            // TODO: Safari Async Support
            var module = await moduleTask;
            var jsClipboardItems = await GetJsClipboardItems(data);
            await module.InvokeVoidAsync("write", jsClipboardItems);
        }

        private async ValueTask<IJSObjectReference> GetJsClipboardItems(IEnumerable<ClipboardItem> clipboardItems)
        {
            var module = await moduleTask;
            return await module.InvokeAsync<IJSObjectReference>("getClipboardItems", clipboardItems.Select(item => new
            {
                item.Items,
                item.Options
            }));
        }

        public async ValueTask<string> ReadText()
        {
            var module = await moduleTask;
            return await module.InvokeAsync<string>("readText");
        }

        public async ValueTask<IEnumerable<ClipboardItem>> Read()
        {
            var module = await moduleTask;
            var jsClipboardItems = await module.InvokeAsync<IJSObjectReference>("read");
            var clipboardItemObjectReferences = await GetArrayObjectReferences(jsClipboardItems);
            return clipboardItemObjectReferences.Select(item => new ClipboardItem(item));
        }

        private async Task<IEnumerable<IJSObjectReference>> GetArrayObjectReferences(IJSObjectReference arrayObjectReference)
        {
            // TODO: Is this really necessary?
            var module = await moduleTask;
            var count = await module.InvokeAsync<int>("getArrayLength", arrayObjectReference);
            var list = new List<IJSObjectReference>();
            for (var i = 0; i < count; i++)
            {
                list.Add(await arrayObjectReference.InvokeAsync<IJSObjectReference>("at", i));
            }

            return list;
        }

        public async ValueTask<bool> IsSupported()
        {
            var module = await moduleTask;
            return await module.InvokeAsync<bool>("isSupported");
        }
        
        public async ValueTask DisposeAsync()
        {
            await (await moduleTask).DisposeAsync();
        }
    }
}