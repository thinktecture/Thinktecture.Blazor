using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using PatrickJahr.Blazor.AsyncClipboard;
using PatrickJahr.Blazor.AsyncClipboard.Models;

namespace PatrickJahr.Blazor.Sample.Pages
{
    public partial class AsyncClipboard
    {
        [Inject] private IJSRuntime _jsRuntime { get; set; } = default!;
        [Inject] private AsyncClipboardService _asyncClipboardService { get; set; } = default!;

        private IJSObjectReference? _module;
        private bool _isAsyncClipboardSupported;

        protected override async void OnInitialized()
        {
            _isAsyncClipboardSupported = await _asyncClipboardService.IsSupportedAsync();
            _module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./Pages/AsyncClipboard.razor.js");
            await InvokeAsync(StateHasChanged);
            await base.OnInitializedAsync();
        }
        
        private async Task CopyText()
        {
            await _asyncClipboardService.WriteTextAsync("Foo");
        }

        private async Task CopyItems()
        {
            var blobPromise = _asyncClipboardService.GetObjectReference(_module!, "getBlazorLogoBlobPromise");
            await _asyncClipboardService.WriteAsync(new []
            {
                new ClipboardItem(new Dictionary<string, IJSObjectReference>
                {
                    { "image/png", blobPromise }
                }, new ClipboardItemOptions { PresentationStyle = PresentationStyle.Attachment })
            });
        }

        private async Task PasteText()
        {
            var data = await _asyncClipboardService.ReadTextAsync();
            Console.WriteLine(data);
        }

        private async Task PasteItems()
        {
            var data = await _asyncClipboardService.ReadAsync();
            var blob = await data.First().GetTypeAsync("image/png");
            await _module!.InvokeVoidAsync("showBlob", blob);
        }
    }
}
