# Thinktecture.Blazor.FileHandling

[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/Thinktecture.Blazor.FileHandling?label=NuGet%20Downloads)](https://www.nuget.org/packages/Thinktecture.Blazor.FileHandling/)

## Introduction

A Blazor wrapper for the [Badging API](https://w3c.github.io/badging/).

The Badging API allows you to share a text, title, URL, or files with another application installed on the user's system via the share functionality provided by the operating system.

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

### Imports

You need to import the package to use it on your pages. This can be achieved by adding the following using statement to `_Imports.razor`:

```
@using Thinktecture.Blazor.Badging
```

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

```csharp
await badgingService.SetAppBadgeAsync();
```

```csharp
await badgingService.SetAppBadgeAsync(3);
```

### Clear app badge

```csharp
await badgingService.ClearAppBadgeAsync();
```

## Related articles

- [Documentation on MDN](https://developer.mozilla.org/en-US/docs/Web/API/Navigator/setAppBadge)
- [Blog post on web.dev](https://web.dev/badging-api/)
- [Browser support on caniuse.com](https://caniuse.com/mdn-api_navigator_setappbadge)

## Acknowledgements

Thanks to [Kristoffer Strube](https://twitter.com/kstrubeg) who provides [a Blazor wrapper for the File System Access API](https://github.com/KristofferStrube/Blazor.FileSystemAccess).
This library is inspired by Kristoffer's implementation and project setup.
