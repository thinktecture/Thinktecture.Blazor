using Thinktecture.Blazor.GrpcDevTools.Shared.DTO;
using Thinktecture.Blazor.GrpcDevTools.Shared.Services;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Thinktecture.Blazor.GrpcDevTools.Client.Features.Conferences.Components
{
    public partial class ConferenceDetails
    {
        [Inject] private IConferencesService _conferencesService { get; set; } = default!;
        [Inject] private NavigationManager _navigationManager { get; set; } = default!;

        [Parameter] public Guid? Id { get; set; }
        [Parameter] public string Mode { get; set; } = string.Empty;

        private ConferenceDetailModel? _conference;
        private DateRange? _dateRange;
        private bool _isNew => Mode == "new";
        protected override async Task OnInitializedAsync()
        {
            if (_isNew)
            {
                _conference = new ConferenceDetailModel();
            } 
            else if (Id.HasValue && Id.Value != Guid.Empty)
            {
                _conference = await _conferencesService.GetConferenceDetailsAsync(new ConferenceDetailsRequest { ID = Id.Value });
                if (_conference is not null)
                {
                    _dateRange = new DateRange(_conference.DateFrom, _conference.DateTo);
                }
            }            
            await base.OnInitializedAsync();
        }

        private async Task SaveConference()
        {
            _conference!.DateFrom = _dateRange!.Start;
            _conference!.DateTo = _dateRange!.End;
            if (_isNew)
            {
                await _conferencesService.AddNewConferenceAsync(_conference);
            }
            else if (Id.HasValue && Id.Value != Guid.Empty)
            {                
                await _conferencesService.UpdateConferenceAsync(
                    new ConferenceUpdateRequest { ID = Id.Value, Conference = _conference });
            }
            _navigationManager.NavigateTo("/");
        }

        private void Cancel() => _navigationManager.NavigateTo("/");
    }
}