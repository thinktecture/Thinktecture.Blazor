using Microsoft.Extensions.DependencyInjection;

namespace Thinktecture.Blazor.ViewTransitions;

public static class ViewTransitionsServiceCollectionExtensions
{
    /// <summary>
    /// Adds all necessary services needed for the ViewTransition API
    /// </summary>
    /// <param name="services"></param>
    public static void AddViewTransitionService(this IServiceCollection services)
    {
        services.AddScoped<IViewTransitionService, ViewTransitionService>();
    }
}