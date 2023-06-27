namespace Thinktecture.Blazor.ViewTransitions;

public interface IViewTransitionService
{
    /// <summary>
    /// Check if the view transition api is supported on your browser
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<bool> IsSupportedAsync(CancellationToken cancellationToken);
        
    /// <summary>
    /// Start view transition with the help of the View Transition API (https://drafts.csswg.org/css-view-transitions-1/) 
    /// </summary>
    /// <param name="beforeTransition">Optinal: Task that is executed before the view transition is executed.</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns></returns>
    Task StartViewTransitionAsync(Task? beforeTransition = null, CancellationToken cancellationToken = default);
}