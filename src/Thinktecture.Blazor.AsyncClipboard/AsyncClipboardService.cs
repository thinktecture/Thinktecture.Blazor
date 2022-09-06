using Microsoft.JSInterop;

namespace Thinktecture.Blazor.AsyncClipboard
{
    public class AsyncClipboardService : IAsyncDisposable
    {
        private readonly Lazy<Task<IJSObjectReference>> moduleTask;

        public AsyncClipboardService(IJSRuntime jsRuntime)
        {
            moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
                "import", "./_content/Thinktecture.Blazor.AsyncClipboard/asyncClipboardService.js").AsTask());
        }

        public async ValueTask WriteText(string data)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("writeText", data);
        }

        public async ValueTask Write(IEnumerable<IClipboardItem> data)
        {
            var module = await moduleTask.Value;
            await module.InvokeVoidAsync("write", data);
        }

        public async ValueTask<string> ReadText()
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<string>("readText");
        }

        public async ValueTask<IEnumerable<IClipboardItem>> Read()
        {
            var module = await moduleTask.Value;
            return await module.InvokeAsync<IEnumerable<IClipboardItem>>("read");
        }

        public async ValueTask DisposeAsync()
        {
            if (moduleTask.IsValueCreated)
            {
                var module = await moduleTask.Value;
                await module.DisposeAsync();
            }
        }
    }
}