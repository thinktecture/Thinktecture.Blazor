using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Thinktecture.Blazor.FileHandling;

namespace Thinktecture.Blazor.Sample.Pages
{
    public partial class FileHandling
    {
        [Inject] private FileHandlingService _fileHandlingService { get; set; } = default!;

        private bool _isFileHandlingSupported;
        private string _text = "";

        protected override async Task OnInitializedAsync()
        {
            _isFileHandlingSupported = await _fileHandlingService.IsSupportedAsync();
            if (_isFileHandlingSupported)
            {
                await _fileHandlingService.SetConsumerAsync(async (p) =>
                {
                    foreach (var fileSystemFileHandle in p.Files)
                    {
                        var file = await fileSystemFileHandle.InvokeAsync<IJSObjectReference>("getFile", null);
                        var text = await file.InvokeAsync<string>("text", null);
                        _text = text;
                        StateHasChanged();
                    }
                });
            }
            
            await base.OnInitializedAsync();
        }
    }
}