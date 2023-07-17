# Thinktecture.Blazor.ScreenWakeLock

[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/Thinktecture.Blazor.ScreenWakeLock?label=NuGet%20Downloads)](https://www.nuget.org/packages/Thinktecture.Blazor.ScreenWakeLock/)

## Introduction

A Blazor wrapper for the [Screen Wake Lock API](https://www.w3.org/TR/screen-wake-lock/).

The Screen Wake Lock API allows web applications to request a screen wake lock. Under the right conditions, and if allowed, the screen wake lock prevents the system from turning off a device's screen.

## Getting started

### Prerequisites

You need .NET 7.0 or newer to use this library.

[Download .NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)

### Platform support

[![Platform support for Screen Wake Lock API](https://caniuse.bitsofco.de/image/wake-lock.png)](https://caniuse.com/?search=wake-lock)

### Installation

You can install the package via NuGet with the Package Manager in your IDE or alternatively using the command line:

```
dotnet add package Thinktecture.Blazor.ScreenWakeLock
```

## Usage

The package can be used in Blazor WebAssembly projects.

### Add to service collection

To make the ScreenWakeLockService available on all pages, register it at the IServiceCollection in `Program.cs` before the host is built:

```csharp
builder.Services.AddScreenWakeLockService();
```

### Checking for browser support

Before using the ScreenWakeLock API, you should first test if the API is supported on the target platform by calling the `IsSupportedAsync()` method.
This method returns a boolean to indicate whether the Web Share API is supported or not.

```csharp
var isSupported = await screenWakeLock.IsSupportedAsync();
if (isSupported)
{
    // enable share feature
}
else
{
    // use fallback mechanism or hide/disable feature
}
```

### Request screen wake lock

To request a screen wake lock, you need to call the `RequestWakeLockAsync` method from the `IScreenWakeLockService` service.
The browser can refuse the request for various reasons (for example, because the battery charge level is too low), 
so it's a good practice to wrap the call in a `try…catch` statement. 
The exception's message will contain more details in case of failure.

```csharp
try 
{
    await _screenWakeLockService.RequestWakeLockAsync();
}
catch(Exception e) 
{
    // Handle exxception
}
```

### Release screen wake lock

You also need a way to release the screen wake lock, which is achieved by calling the `ReleaseWakeLockAsync` method of the `IScreenWakeLockService` service.

```csharp
await _screenWakeLockService.ReleaseWakeLockAsync();
```

As soon as the object was released the action `WakeLockReleased` will be fired.

```csharp
protected override async Task OnInitializedAsync()
{
    //...
    
    _screenWakeLockService.WakeLockReleased = () =>
    {
       _wakeLockRequested = false;
    };
    
    //...
    await base.OnInitializedAsync();
}
```


## Related articles

- [Documentation on MDN](https://developer.mozilla.org/en-US/docs/Web/API/WakeLock)
- [Blog post on web.dev](https://developer.chrome.com/articles/wake-lock/)
- [Browser support on caniuse.com](https://caniuse.com/web-lock)

## Acknowledgements

Thanks to [Kristoffer Strube](https://twitter.com/kstrubeg) who provides [a Blazor wrapper for the File System Access API](https://github.com/KristofferStrube/Blazor.FileSystemAccess).
This library is inspired by Kristoffer's implementation and project setup.

## License and Note

BSD-3-Clause.

This is a technical showcase, not an official Thinktecture product.
