# Thinktecture.Blazor.Badging

[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/Thinktecture.Blazor.Badging?label=NuGet%20Downloads)](https://www.nuget.org/packages/Thinktecture.Blazor.Badging/)

## Introduction

A Blazor wrapper for the [Badging API](https://w3c.github.io/badging/).

The Badging API allows you to display a badge on the installed application’s icon.

## Getting started

### Prerequisites

You need .NET 6.0 or newer to use this library.

[Download .NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)

### Platform support

[Platform support for Badging API](https://caniuse.com/mdn-api_navigator_setappbadge)

### Installation

You can install the package via NuGet with the Package Manager in your IDE or alternatively using the command line:

```
dotnet add package Thinktecture.Blazor.Badging
```

## Usage

The package can be used in Blazor WebAssembly projects.

### Add to service collection

To make the BadgingService available on all pages, register it at the IServiceCollection in `Program.cs` before the host is built:

```csharp
builder.Services.AddBadgingService();
```

### Checking for browser support

Before using the Badging API, you should first test if the API is supported on the target platform by calling the `IsSupportedAsync()` method.
This method returns a boolean to indicate whether the Badging API is supported or not.

```csharp
var isSupported = await badgingService.IsSupportedAsync();
if (isSupported)
{
    // enable badging feature
}
else
{
    // use fallback mechanism or hide/disable feature
}
```

Internally, this method tests for the presence of the `setAppBadge()` and `clearAppBadge()` methods on the `navigator` object of the target browser.

### Set app badge

The `SetAppBadgeAsync()` method sets a badge on the current app's icon.
If a value is passed to this method, it will be set as the value of the badge:

```csharp
await badgingService.SetAppBadgeAsync(3);
```

When omitting the `contents` parameter, a generic badge will be shown, as defined by the platform.

```csharp
await badgingService.SetAppBadgeAsync();
```

### Clear app badge

The `ClearAppBadgeAsync()` method clears the badge on the current app's icon:

```csharp
await badgingService.ClearAppBadgeAsync();
```

## Related articles

- [setAppBadge() Documentation on MDN](https://developer.mozilla.org/en-US/docs/Web/API/Navigator/setAppBadge)
- [clearAppBadge() Documentation on MDN](https://developer.mozilla.org/en-US/docs/Web/API/Navigator/clearAppBadge)
- [Blog post on web.dev](https://web.dev/badging-api/)
- [Browser support on caniuse.com](https://caniuse.com/mdn-api_navigator_setappbadge)

## Acknowledgements

Thanks to [Kristoffer Strube](https://twitter.com/kstrubeg) who provides [a Blazor wrapper for the File System Access API](https://github.com/KristofferStrube/Blazor.FileSystemAccess).
This library is inspired by Kristoffer's implementation and project setup.
