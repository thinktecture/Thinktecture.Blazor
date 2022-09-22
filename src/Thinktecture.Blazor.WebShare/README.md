# Thinktecture.Blazor.WebShare

[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/Thinktecture.Blazor.WebShare?label=NuGet%20Downloads)](https://www.nuget.org/packages/Thinktecture.Blazor.WebShare/)

## Introduction

A Blazor wrapper for the [Web Share API](https://www.w3.org/TR/web-share/).

The Web Share API allows you to share a text, title, URL, or files with another application installed on the user's system via the share functionality provided by the operating system.

## Getting Started

### Prerequisites

You need .NET 6.0 or newer to use this library.

[Download .NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)

### Installation

You can install the package via NuGet with the Package Manager in your IDE or alternatively using the command line:

```
dotnet add package Thinktecture.Blazor.WebShare
```

## Usage

The package can be used in Blazor WebAssembly projects.

### Imports

You need to import the package to use it on your pages. This can be achieved by adding the following using statement to `_Imports.razor`:

```
@using Thinktecture.Blazor.WebShare
```

### Add to service collection

To make the WebShareService available on all pages, register it at the IServiceCollection in `Program.cs` before the host is built:

```csharp
builder.Services.AddWebShareService();
```

### Checking for browser support

Before using the Web Share API, you should first test if the API is supported on the target platform by calling the `IsSupportedAsync()` method.
This method returns a boolean to indicate whether the Web Share API is supported or not.

```csharp
var isSupported = await webShareService.IsSupportedAsync();
if (isSupported)
{
    // enable share feature
}
else
{
    // use fallback mechanism or hide/disable feature
}
```

Internally, this method tests for the presence of the `share()` and `canShare()` methods on the `navigator` object of the target browser.
Please note that the `canShare()` method and file sharing capability were added later during the specification process.
Both methods are part of the W3C Candidate Recommendation and supported by all recent version of browsers that are shipping with support for this API, which is why we are testing for both.
If you want to support legacy browsers that ship with support for `share()`, but not for sharing files and `canShare()`, please implement a custom check.

### Checking for share support

Before trying to share data, you should first test if the browser supports sharing the particular data, as the browser may not support sharing certain file formats.
The `CanShareAsync()` method returns a boolean value that determines if the data you want to share is actually supported.
This method takes an argument of the type `WebShareDataModel`.
This is an object that contains a `Title`, `Text`, `Url`, and `Files`.
All properties are optional, but at least one property must be set.

```csharp
var data = new WebShareDataModel
{
    Title = "Test 1",
    Text = "Lorem ipsum dolor...",
    Url = "https://thinktecture.com"
};
var canShare = await webShareService.CanShareAsync(data);
if (canShare)
{
    // call ShareAsync()
}
else
{
    // use fallback mechanism or hide/disable share feature
}
```

Please note that the `CanShareAsync()` method throws an exception if the `canShare()` JavaScript method is not present in the browser, so make sure that the browser supports the Web Share API first by calling `IsSupportedAsnyc()`.

### Sharing data

To share the data, call the `ShareAsync()` method and pass an instance of `WebShareDataModel` to it.
Please note that this method may throw an exception in case the `share()` method is not supported by the target platform, the user agent does not support sharing the data, or the user denied sharing it (e.g., by dismissing the share sheet). 

```csharp
try
{
    var data = new WebShareDataModel
    {
        Title = "Test 1",
        Text = "Lorem ipsum dolor...",
        Url = "https://thinktecture.com"
    };
    await webShareService.ShareAsync(data);
}
catch (Exception ex)
{
    // method does not exist on target platform,
    // data not shareable or user denied sharing
}
```

### Sharing files

Sharing files is supported via the `Files` property.
It takes a list of `IJSObjectReference`s that point to JavaScript [`File`](https://developer.mozilla.org/en-US/docs/Web/API/File) objects.

The following JavaScript function generates a plain text file with `foo` as its content:

```js
export function generateSampleFile() {
    const blob = new Blob(["foo"], { type: 'text/plain' });
    return new File([blob], 'text.txt', { type: blob.type });
}
```

In C#, the reference to this `File` object can be passed to the `Files` property as follows:

```csharp
try
{
    var file = await _module.InvokeAsync<IJSObjectReference>("generateSampleFile");
    var data = new WebShareDataModel
    {
        Files = new [] { file }
    };
    await webShareService.ShareAsync(data);
}
catch (Exception ex)
{
    // method does not exist on target platform,
    // data not shareable or user denied sharing
}
```

Please note that `ShareAsnyc()` may throw an exception for the aforementioned reasons.

## Related articles

- [Documentation on MDN](https://developer.mozilla.org/en-US/docs/Web/API/Navigator/share)
- [Blog post on web.dev](https://web.dev/web-share/)
- [Browser support on caniuse.com](https://caniuse.com/web-share)

## Acknowledgements

Thanks to [Kristoffer Strube](https://twitter.com/kstrubeg) who provides [a Blazor wrapper for the File System Access API](https://github.com/KristofferStrube/Blazor.FileSystemAccess).
This library is inspired by Kristoffer’s implementation and project setup.
