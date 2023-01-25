# Thinktecture.Blazor.AsyncClipboard

[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/Thinktecture.Blazor.AsyncClipboard?label=NuGet%20Downloads)](https://www.nuget.org/packages/Thinktecture.Blazor.AsyncClipboard/)

## Introduction

A Blazor wrapper for the [Async Clipboard API](https://www.w3.org/TR/clipboard-apis/).

The Async Clipboard API allows you to write and read text, images and other data from or to the system's clipboard.
The supported types vary from platform to platform.

## Getting started

### Prerequisites

You need .NET 7.0 or newer to use this library.

[Download .NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)

### Platform support

[![Platform support for Async Clipboard API](https://caniuse.bitsofco.de/image/async-clipboard.png)](https://caniuse.com/async-clipboard)

### Installation

You can install the package via NuGet with the Package Manager in your IDE or alternatively using the command line:

```
dotnet add package Thinktecture.Blazor.AsyncClipboard
```

## Usage

The package can be used in Blazor WebAssembly projects.

### Add to service collection

To make the AsyncClipboardService available on all pages, register it at the IServiceCollection in `Program.cs` before the host is built:

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

### Writing text to the clipboard

To write text to the clipboard, use the `WriteTextAsync()` method:

```csharp
await asyncClipboardService.WriteTextAsync("Hello world");
```

Please note that writing to the clipboard may fail, e.g., because the user denied the permission to access the clipboard.

### Writing data to the clipboard

To write text to the clipboard, use the `WriteAsync()` method.
The method takes a list of clipboard items.
Each clipboard item can take multiple representations of the same item.
It so takes a dictionary of media types matched to an IJSObjectReference that points to either a JavaScript string or Blob.
Optionally, you can pass `ClipboardItemOptions` to the item.
Currently, this only allows you to define the presentation style of the pasted item, i.e., if the content should be added inline or as an attachment.
If no value is given, the `PresentationStyle` property contains the value `"unspecified"`.

> **Important note:** All items must be passed synchronously to the `WriteAsync()` method, i.e., it _must_ be the first awaited call within your event handler.
> If you need to perform asynchronous work to determine the Blob, for example, because you need to scale down image data, perform this logic in a promise.
> For this purpose, the library offers a `GetObjectReference()` helper method that synchronously returns an `IJSObjectReference`, for example, to point this to a promise.

```csharp
var textPromise = asyncClipboardService.GetObjectReference(module, "getTextPromise");
var items = new []
{
    new ClipboardItem(new Dictionary<string, IJSObjectReference>
    {
        { "text/plain", textPromise }
    }, new ClipboardItemOptions { PresentationStyle = PresentationStyle.Inline })
};
await asyncClipboardService.WriteAsync(items);
```

With the following JavaScript code:

```js
export function getTextPromise() {
    return new Promise((resolve) => resolve("Hello world"));
}
```

Please note that writing to the clipboard may fail, e.g., because the user denied the permission to access the clipboard.

### Reading text from the clipboard

To read text from the clipboard, use the `ReadTextAsync()` method:

```csharp
try
{
    var text = await asyncClipboardService.ReadTextAsync();
    // do something with the text
}
catch (Exception ex)
{
    // pasting failed (e.g., user denied permission)
}
```

Please note that the user may need to confirm a permission request first, and reading may fail, e.g., because the user denied the permission to access the clipboard.

### Reading data from the clipboard

To write text to the clipboard, use the `ReadAsync()` method:

```csharp
var clipboardItems = await asyncClipboardService.ReadAsync();
var imageItem = clipboardItems.FirstOrDefault(c => c.Types.Contains("image/png"));
if (imageItem is not null)
{
    try
    {
        var pngBlob = await imageItem.GetTypeAsync("image/png");
        // do something with the data
    }
    catch (Exception ex)
    {
        // error while retrieving the data
    }
}
```

Please note that the user may need to confirm a permission request first, and reading may fail, e.g., because the user denied the permission to access the clipboard.

## Related articles

- [Documentation on MDN](https://developer.mozilla.org/en-US/docs/Web/API/Clipboard)
- [Blog post on web.dev](https://web.dev/async-clipboard/)
- [Blog post on webkit.org](https://webkit.org/blog/10855/async-clipboard-api/)
- [Browser support on caniuse.com](https://caniuse.com/async-clipboard)

## Acknowledgements

Thanks to [Kristoffer Strube](https://twitter.com/kstrubeg) who provides [a Blazor wrapper for the File System Access API](https://github.com/KristofferStrube/Blazor.FileSystemAccess).
This library is inspired by Kristoffer's implementation and project setup.

## License and Note

BSD-3-Clause.

This is a technical showcase, not an official Thinktecture product.
