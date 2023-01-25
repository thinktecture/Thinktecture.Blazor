using KristofferStrube.Blazor.FileSystem;
using Microsoft.AspNetCore.Components;
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
                    foreach (var fileSystemHandle in p.Files)
                    {
                        if (fileSystemHandle is FileSystemFileHandle fileSystemFileHandle)
                        {
                            var file = await fileSystemFileHandle.GetFileAsync();
                            var text = await file.TextAsync();
                            _text = text;
                            StateHasChanged();
                        }
                    }
                });
            }
            
            await base.OnInitializedAsync();
        }
    }
}