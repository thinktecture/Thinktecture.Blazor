using Thinktecture.Blazor.GrpcDevTools.Client.Features.Shared.Dialogs;
using Thinktecture.Blazor.GrpcDevTools.Shared.DTO;
using Thinktecture.Blazor.GrpcDevTools.Shared.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Thinktecture.Blazor.GrpcDevTools.Client.Features.Conferences
{
    public partial class Conferences
    {
        [Inject] private IConferencesService _conferencesService { get; set; } = default!;
        [Inject] private NavigationManager _navigationManager { get; set; } = default!;
        [Inject] private IDialogService _dialogService { get; set; } = default!;

        private List<ConferenceOverview>? _conferences;

        protected override async Task OnInitializedAsync()
        {
            _conferences = (await _conferencesService.ListConferencesAsync()).ToList();
            await base.OnInitializedAsync();
        }

        private void AddConference()
        {
            _navigationManager.NavigateTo($"/conferences/new");
        }

        private void EditConference(Guid id)
        {
            _navigationManager.NavigateTo($"/conferences/edit/{id}");
        }

        private async Task DeleteConference(Guid id, string title)
        {
            var parameters = new DialogParameters();
            parameters.Add(nameof(ConfirmDialog.ContentText), $"Are you sure you want to remove {title}?");
            parameters.Add(nameof(ConfirmDialog.ButtonText), "Yes");
            parameters.Add(nameof(ConfirmDialog.Color), Color.Success);

            var reference = _dialogService.Show<ConfirmDialog>("Delete", parameters);
            var result = await reference.Result;
            if (result.Data is bool confirmed && confirmed)
            {
                await _conferencesService.DeleteConferenceAsync(new ConferenceDetailsRequest() { ID = id });
                _conferences = (await _conferencesService.ListConferencesAsync()).ToList();
            }
        }
    }
}