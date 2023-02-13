using Microsoft.AspNetCore.Components;
using Thinktecture.Blazor.PwaUpdate.Services;

namespace Thinktecture.Blazor.PwaUpdate
{
    public partial class UpdateModal
    {
        [Inject] private UpdateService _updateService { get; set; } = default!;

        [Parameter] public string InformationMessage { get; set; } = string.Empty;
        [Parameter] public RenderFragment? ChildContent { get; set; }

        private bool _newVersionAvailable = false;
        private bool _collapse = false;

        protected override async Task OnInitializedAsync()
        {
            _updateService.UpdateAvailable = () => _newVersionAvailable = true;
            await _updateService.InitializeServiceWorkerUpdate();
            await base.OnInitializedAsync();
        }

        private void CollapseModal()
        {
            _collapse = true;
        }
    }
}