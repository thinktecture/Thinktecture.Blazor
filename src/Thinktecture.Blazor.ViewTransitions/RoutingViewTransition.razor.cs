using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace Thinktecture.Blazor.ViewTransitions;

public partial class RoutingViewTransition
{
    [Inject] private IViewTransitionService _viewTransitionService { get; set; } = default!;
    [Inject] private NavigationManager _navigationManager { get; set; } = default!;

    private TaskCompletionSource _taskCompletionSource = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

    protected override void OnInitialized()
    {
        _navigationManager.RegisterLocationChangingHandler(async (context) => { await StartTransition(context); });
        _navigationManager.LocationChanged += _navigationManager_LocationChanged;

    }

    private void _navigationManager_LocationChanged(object? sender, LocationChangedEventArgs e)
    {
        _taskCompletionSource.SetResult();
    }

    private async Task StartTransition(LocationChangingContext context)
    {
        if (await _viewTransitionService.IsSupportedAsync(context.CancellationToken))
        {
            _taskCompletionSource = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);
            await _viewTransitionService.StartViewTransitionAsync(_taskCompletionSource.Task, context.CancellationToken);
        }

    }
}