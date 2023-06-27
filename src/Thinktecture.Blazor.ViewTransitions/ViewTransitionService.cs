using Microsoft.JSInterop;

namespace Thinktecture.Blazor.ViewTransitions;

public class ViewTransitionService : IViewTransitionService
{
    private readonly Lazy<ValueTask<IJSInProcessObjectReference>> _moduleTask;
    private TaskCompletionSource _oldViewStateCompleted = new(TaskCreationOptions.RunContinuationsAsynchronously);
    private Task? _beforeTransition;

    public ViewTransitionService(IJSRuntime jSRuntime)
    {
        _moduleTask = new(() => jSRuntime.InvokeAsync<IJSInProcessObjectReference>(
            "import", "./_content/Thinktecture.Blazor.ViewTransitions/Thinktecture.Blazor.ViewTransitions.js"));
    }

    public async Task<bool> IsSupportedAsync(CancellationToken cancellationToken = default)
    {
        var module = await _moduleTask.Value;
        return await module.InvokeAsync<bool>("isSupported", cancellationToken);
    }

    /// <summary>
    /// Start view transition with the help of the View Transition API (https://drafts.csswg.org/css-view-transitions-1/) 
    /// </summary>
    /// <param name="beforeTransition">Optinal: Task that is executed before the view transition is executed.</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns></returns>
    public async Task StartViewTransitionAsync(Task? beforeTransition = null, CancellationToken cancellationToken = default)
    {
        var module = await _moduleTask.Value;
        _beforeTransition = beforeTransition;
        await module.InvokeVoidAsync("startViewTransition", cancellationToken, DotNetObjectReference.Create(this), nameof(TransitionStarted));
        await _oldViewStateCompleted.Task;
        _oldViewStateCompleted = new TaskCompletionSource(TaskCreationOptions.RunContinuationsAsynchronously);

    }

    [JSInvokable]
    public async Task TransitionStarted()
    {
        _oldViewStateCompleted.SetResult();
        if (_beforeTransition is not null)
        {
            await _beforeTransition;
        }
    }
}