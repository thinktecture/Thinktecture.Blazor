# Thinktecture.Blazor.AsyncClipboard

[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/Thinktecture.Blazor.AsyncClipboard?label=NuGet%20Downloads)](https://www.nuget.org/packages/Thinktecture.Blazor.AsyncClipboard/)

## Introduction

A Blazor wrapper for the [Async Clipboard API](https://www.w3.org/TR/clipboard-apis/).

The Async Clipboard API allows you to copy and paste text, images and other data from or to the system's clipboard.

## Getting Started

### Prerequisites

You need .NET 6.0 or newer to use this library.

[Download .NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)

### Platform Support

![Platform support for Async Clipboard API](https://caniuse.bitsofco.de/image/async-clipboard.png)

### Installation

You can install the package via NuGet with the Package Manager in your IDE or alternatively using the command line:

```
dotnet add package Thinktecture.Blazor.AsyncClipboard
```

## Usage

The package can be used in Blazor WebAssembly projects.

### Imports

You need to import the package to use it on your pages. This can be achieved by adding the following using statement to `_Imports.razor`:

```
@using Thinktecture.Blazor.AsyncClipboard
```

### Add to service collection

To make the WebShareService available on all pages, register it at the IServiceCollection in `Program.cs` before the host is built:

```csharp
builder.Services.AddAsyncClipboardService();
```

### Checking for browser support

Before using the Async Clipboard API, you should first test if the API is supported on the target platform by calling the `IsSupportedAsync()` method.
This method returns a boolean to indicate whether the Async Clipboard API is supported or not.

```csharp
var isSupported = await asyncClipboardService.IsSupportedAsync();
if (isSupported)
{
    // enable clipboard feature
}
else
{
    // use fallback mechanism or hide/disable feature
}
```

Internally, this method tests for the presence of the `write()` method on the `navigator.clipboard` object of the target browser.
This method allows copying (more or less) arbitrary data to the clipboard. 
Please note that Firefox only supports the `writeText()` method which writes plain text to the clipboard.
If you want to support browsers that only ship with support for `writeText()`, please implement a custom check.

### TODO

## Related articles

- [Documentation on MDN](https://developer.mozilla.org/en-US/docs/Web/API/Clipboard)
- [Blog post on web.dev](https://web.dev/async-clipboard/)
- [Blog post on webkit.org](https://webkit.org/blog/10855/async-clipboard-api/)
- [Browser support on caniuse.com](https://caniuse.com/async-clipboard)

## Acknowledgements

Thanks to [Kristoffer Strube](https://twitter.com/kstrubeg) who provides [a Blazor wrapper for the File System Access API](https://github.com/KristofferStrube/Blazor.FileSystemAccess).
This library is inspired by Kristoffer’s implementation and project setup.
