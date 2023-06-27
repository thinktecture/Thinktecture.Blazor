using System.Net.Http.Json;
using Fluxor;
using Microsoft.AspNetCore.Components;
using Thinktecture.Blazor.Sample.Models;
using Thinktecture.Blazor.Sample.Store;
using Thinktecture.Blazor.ViewTransitions;

namespace Thinktecture.Blazor.Sample.Pages;

public partial class ViewTransitions
{
    [Inject] private NavigationManager NavigationManager { get; set; } = default!;
    [Inject] private IState<ImageState> _state { get; set; } = default!;
    [Inject] private IDispatcher _dispatcher { get; set; } = default!;
    [Inject] private IViewTransitionService _viewTransitionService { get; set; } = default!;
    
    // page props
    private bool _loading => _state.Value.Loading;
    private bool _openDialog => _state.Value.ShowDialog;
    private IEnumerable<User> _users => _state.Value?.Users ?? Enumerable.Empty<User>();
    private User? _selectedUser => _state.Value?.SelectedUser;
    
    // Dialog props
    private User? _dialogUser;
    private bool _showDialog;

    protected override async Task OnInitializedAsync()
    {
        if (_state.Value?.Users.Count <= 0)
        {
            _dispatcher.Dispatch(new LoadUsersAction());
        }
        await base.OnInitializedAsync();
    }

    private void ToggleViewMode()
    {
        _dispatcher.Dispatch(new ToggleViewModeAction());
    }

    private async Task OpenDetails(User user)
    {
        if (_openDialog)
        {
            await ShowDialog(user);
        }
        else
        {
            GoToDetails(user);
        }
    }

    private void GoToDetails(User user)
    {
        _dispatcher.Dispatch(new SelectUserAction(user));
        NavigationManager.NavigateTo($"/view-transitions/details");
    }

    private async Task ShowDialog(User user)
    {
        await _viewTransitionService.StartViewTransitionAsync(InternalShowDialog(user), CancellationToken.None);
    }

    private async Task InternalShowDialog(User user)
    {
        _dialogUser = user;
        _dispatcher.Dispatch(new SelectUserAction(user));
        await Task.Delay(32);
        _dispatcher.Dispatch(new SelectUserAction(null));
        await Task.Delay(32);
        _showDialog = true;
        StateHasChanged();
    }

    private async Task CloseDialog()
    {
        await _viewTransitionService.StartViewTransitionAsync(InternalCloseDialog(), CancellationToken.None);
    }

    private async Task InternalCloseDialog()
    {
        await Task.Delay(32);
        _showDialog = false;
        _dispatcher.Dispatch(new SelectUserAction(_dialogUser));
    }
}