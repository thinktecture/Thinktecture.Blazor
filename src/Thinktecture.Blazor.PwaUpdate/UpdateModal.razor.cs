using Microsoft.AspNetCore.Components;
using Thinktecture.Blazor.PwaUpdate.Services;

namespace Thinktecture.Blazor.PwaUpdate
{
    public partial class UpdateModal
    {
        [Inject] private IUpdateService _updateService { get; set; } = default!;

        [Parameter] public string InformationMessage { get; set; } = string.Empty;
        [Parameter] public RenderFragment? ChildContent { get; set; }

        private bool _newVersionAvailable = false;
        private bool _collapse = false;

        protected override async Task OnInitializedAsync()
        {
            _updateService.UpdateAvailable = () => _newVersionAvailable = true;
            await _updateService.InitializeServiceWorkerUpdateAsync();
            await base.OnInitializedAsync();
        }

        private async Task Reload()
        {
            await _updateService.ReloadAsync();
        }

        private void CollapseModal()
        {
            _collapse = true;
        }
    }
}