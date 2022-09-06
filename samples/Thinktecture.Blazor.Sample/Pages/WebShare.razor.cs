using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Thinktecture.Blazor.WebShare.Models;
using Thinktecture.Blazor.WebShare;

namespace Thinktecture.Blazor.Sample.Pages
{
    public partial class WebShare
    {
        [Inject] private IJSRuntime _jsRuntime { get; set; } = default!;
        [Inject] private WebShareService _webShareService { get; set; } = default!;

        private IJSObjectReference? _module;

        private readonly WebShareDataModel _sampleData = new WebShareDataModel
        {
            Title = "Test 1",
            Text = "Lorem ipsum dolor...",
            Url = "https://thinktecture.com"
        };

        private bool _isWebShareSupported = false;
        private bool _canShareBasicData = false;
        private bool _canShareFileData = false;

        protected override async Task OnInitializedAsync()
        {
            _isWebShareSupported = await _webShareService.IsSupportedAsync();
            _canShareBasicData = await _webShareService.CanShareAsync(_sampleData);
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                _module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./Pages/WebShare.razor.js");
                var file = await _module.InvokeAsync<IJSObjectReference>("generateSampleFile");
                _sampleData.Files = new[] { file };
                _canShareFileData = await _webShareService.CanShareAsync(_sampleData);

                await InvokeAsync(StateHasChanged);
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task ShareData()
        {
            if (_module is not null)
            {
                try
                {
                    var file = await _module.InvokeAsync<IJSObjectReference>("generateSampleFile");
                    await _webShareService.ShareAsync(_sampleData);
                }
                catch (Exception e)
                {
                    await _jsRuntime.InvokeVoidAsync("alert", e.Message);
                }
            }
        }
    }
}