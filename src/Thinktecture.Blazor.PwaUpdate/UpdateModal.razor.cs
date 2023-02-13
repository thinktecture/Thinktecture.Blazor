using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Thinktecture.Blazor.PwaUpdate
{
    public partial class UpdateModal
    {
        [Inject] private IJSRuntime _jsRuntime { get; set; } = default!;

        [Parameter] public string InformationMessage { get; set; } = string.Empty;
        [Parameter] public RenderFragment? ChildContent { get; set; }

        private bool _newVersionAvailable = false;
        private bool _collapse = false;

        protected override async Task OnInitializedAsync()
        {
            var module = await _jsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/Thinktecture.Blazor.PwaUpdate/UpdateModal.razor.js");
            if (module is not null)
            {
                await RegisterForUpdateAvailableNotification(module);
            }
            await base.OnInitializedAsync();
        }

        private async Task RegisterForUpdateAvailableNotification(IJSObjectReference jSObject)
        {
            await jSObject.InvokeVoidAsync("registerUpdateEvent", DotNetObjectReference.Create(this), nameof(OnUpdateAvailable));
        }

        private void CollapseModal()
        {
            _collapse = true;
        }

        [JSInvokable(nameof(OnUpdateAvailable))]
        public void OnUpdateAvailable()
        {
            _newVersionAvailable = true;
            StateHasChanged();
        }
    }
}