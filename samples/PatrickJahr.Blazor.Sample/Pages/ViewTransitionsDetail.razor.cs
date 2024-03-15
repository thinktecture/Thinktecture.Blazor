using Fluxor;
using Microsoft.AspNetCore.Components;
using PatrickJahr.Blazor.Sample.Models;
using PatrickJahr.Blazor.Sample.Store;

namespace PatrickJahr.Blazor.Sample.Pages;

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