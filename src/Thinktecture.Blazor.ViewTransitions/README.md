# Thinktecture.Blazor.ViewTransitions

[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/Thinktecture.Blazor.ViewTransitions?label=NuGet%20Downloads)](https://www.nuget.org/packages/Thinktecture.Blazor.ViewTransitions/)

## Introduction
This package should help you to use the View Transition API in your Blazor application. The package contains two ways to use the View Transition API.
If you want to know how the View Transition API works look [here](https://drafts.csswg.org/css-view-transitions/#viewtransition).

https://github.com/thinktecture/Thinktecture.Blazor/assets/16818441/5a56379d-5e82-4f4c-a228-e8f2a6598254

## Getting started

### Prerequisites

You need .NET 7.0 or newer to use this library.

[Download .NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)

### Platform/Browser support

Please note that View Transition API is not yet supported in all major browsers. [Here](https://caniuse.com/mdn-api_viewtransition), you can find the current support.

### Installation


You can install the package via NuGet with the Package Manager in your IDE or alternatively using the command line:

```
dotnet add package Thinktecture.Blazor.ViewTransitions
```


## Usage

The package can be used in Blazor WebAssembly projects.

### Add to service collection

To make the IViewTransitionService available on all pages, register it at the IServiceCollection in `Program.cs` before the host is built:

`builder.Services.AddViewTransitionService();`

### Add to Imports

To use the default `RoutingViewTransition` component on the hole app razor files, register it in the `_Imports.razor` file.

```html
@using Pazor.ViewTransitionsApi
```

### Routing

For this, you must add the component `RoutingViewTransition` to App.razor.

```html
<!-- App.razor -->
<RoutingViewTransition />

<Router AppAssembly="@typeof(App).Assembly">
    <Found Context="routeData">
        <RouteView RouteData="@routeData" DefaultLayout="@typeof(MainLayout)" />
        <FocusOnNavigate RouteData="@routeData" Selector="h1" />
    </Found>
    <NotFound>
        <PageTitle>Not found</PageTitle>
        <LayoutView Layout="@typeof(MainLayout)">
            <p role="alert">Sorry, there's nothing at this address.</p>
        </LayoutView>
    </NotFound>
</Router>
```

### Manual view transition

The second option is to start the View Transition API using the IViewTransitionService.
The following steps are necessary for this:
- Add the IViewTransitionService to the component or class using DependencyInjection.

`[Inject] private IViewTransitionService _viewTransitionService { get; set; } = default!;`

- To perform a view transition, use the method `StartViewTransitionAsync()`. This takes two parameters.

    - The first parameter is a task. This Task specifies when the transition can be performed. That means the new view is ready, and the transition can start. Please note that the ViewTransitionAPI must first take a screenshot of the current state before the DOM is changed. The following example opens a dialog. Method `StartViewTransitionAsync`, passed as a task, first waits briefly before setting the necessary parameters.

    ```csharp
    private async Task ShowDialog(User user)
    {
        await _viewTransitionService.StartViewTransitionAsync(
            InternalShowDialog(user), 
            CancellationToken.None);
    }

    private async Task InternalShowDialog(User user)
    {
        // New user is set and will dispatched
        _dialogUser = user;
        _dispatcher.Dispatch(new SelectUserAction(user));
        // Wait for the first Screenshot of the current state
        await Task.Delay(32);
        // Reset the selected user. Because a view-transition-name css property may appear only once in the DOM.
        _dispatcher.Dispatch(new SelectUserAction(null));
        // Wait until the state has changed.
        await Task.Delay(32);
        _showDialog = true;
        StateHasChanged();
    }
    ```

    - The second parameter is the `CancellationToken` to cancel the operation.

That's it. Just try it out, and feel free to give feedback :-)

## License and Note

BSD-3-Clause.

This is a technical showcase, not an official product.
