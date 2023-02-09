using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Thinktecture.Blazor.PwaUpdate
{
    public partial class UpdateModal
    {
        [Inject] private IJSRuntime _jsRuntime { get; set; } = default!;

        private bool _newVersionAvailable = true;
        private bool _close = false;

        protected override async Task OnInitializedAsync()
        {
            await RegisterForUpdateAvailableNotification();
        }

        private async Task RegisterForUpdateAvailableNotification()
        {
            await _jsRuntime.InvokeVoidAsync(
                "registerForUpdateAvailableNotification",
                DotNetObjectReference.Create(this),
                nameof(OnUpdateAvailable));
        }

        private void CloseModal()
        {
            _close = true;
        }

        [JSInvokable(nameof(OnUpdateAvailable))]
        public void OnUpdateAvailable()
        {
            _newVersionAvailable = true;
            StateHasChanged();
        }
    }
}