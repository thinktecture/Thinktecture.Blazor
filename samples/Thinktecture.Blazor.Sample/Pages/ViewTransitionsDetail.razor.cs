using Fluxor;
using Microsoft.AspNetCore.Components;
using Thinktecture.Blazor.Sample.Models;
using Thinktecture.Blazor.Sample.Store;

namespace Thinktecture.Blazor.Sample.Pages;

public partial class ViewTransitionsDetail
{
    [Inject] private NavigationManager _navigationManager { get; set; } = default!;
    [Inject] private IState<ImageState> _state { get; set; } = default!;

    private User? _user => _state.Value.SelectedUser;

    private void GoBack()
    {
        _navigationManager.NavigateTo("/view-transitions");
    }
}