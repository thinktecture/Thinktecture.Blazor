using Microsoft.JSInterop;

namespace Thinktecture.Blazor.ImageHelper
{
    public class ImageHelperService
    {
        private readonly Lazy<ValueTask<IJSInProcessObjectReference>> _moduleTask;

        public ImageHelperService(IJSRuntime jsRuntime)
        {
            _moduleTask = new(() => jsRuntime.InvokeAsync<IJSInProcessObjectReference>(
                "import", "./_content/Thinktecture.Blazor.ImageHelper/Thinktecture.Blazor.ImageHelper.js"));
        }

        public async Task OpenAsync(Stream stream)
        {
            var module = await _moduleTask.Value;
            var currentStream = new DotNetStreamReference(stream);
            await module.InvokeVoidAsync("createImage", currentStream);
        }

        public async Task CreateImageFromFileReferenceAsync(IJSObjectReference fileReference)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("createImageFromFile", fileReference);
        }

        public async Task SaveAsync(string dataUrl)
        {
            var module = await _moduleTask.Value;
            await module.InvokeVoidAsync("saveImage", dataUrl);
        }

        public async Task<byte[]> GetImageDataAsync(string canvasRefId)
        {
            var module = await _moduleTask.Value;
            return await module.InvokeAsync<byte[]>("getCanvasImageData", canvasRefId);
        }
    }
}
