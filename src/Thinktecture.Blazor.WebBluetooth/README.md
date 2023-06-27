# Thinktecture.Blazor.WebBluetooth

[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/Thinktecture.Blazor.WebBluetooth?label=NuGet%20Downloads)](https://www.nuget.org/packages/Thinktecture.Blazor.WebBluetooth/)

## Introduction

A Blazor wrapper for the [Web Bluetooth API](https://www.w3.org/community/web-bluetooth/).

The Web Bluetooth API allows you to ...

## Getting started

### Prerequisites

You need .NET 7.0 or newer to use this library.

[Download .NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)

### Platform support

[![Platform support for Web Share API](https://caniuse.bitsofco.de/image/web-bluetooth.png)](https://caniuse.com/web-bluetooth)

### Installation

You can install the package via NuGet with the Package Manager in your IDE or alternatively using the command line:

```
dotnet add package Thinktecture.Blazor.WebBluetooth
```

## Usage

The package can be used in Blazor WebAssembly projects.

### Add to service collection

To make the WebBluetoothService available on all pages, register it at the IServiceCollection in `Program.cs` before the host is built:

```csharp
builder.Services.AddWebBluetoothService();
```

### Checking for browser support

Before using the Web Bluetooth API, you should first test if the API is supported on the target platform by calling the `IsSupportedAsync()` method.
This method returns a boolean to indicate whether the Web Bluetooth API is supported or not.

```csharp
var isSupported = await webBluetoothService.IsSupportedAsync();
if (isSupported)
{
    // enable share feature
}
else
{
    // use fallback mechanism or hide/disable feature
}
```


## Related articles

- [Documentation on MDN](https://developer.mozilla.org/en-US/docs/Web/API/Web_Bluetooth_API)
- [Blog post on web.dev](https://web.dev/web-bluetooth/)
- [Browser support on caniuse.com](https://caniuse.com/web-bluetooth)

## Acknowledgements

Thanks to [Kristoffer Strube](https://twitter.com/kstrubeg) who provides [a Blazor wrapper for the File System Access API](https://github.com/KristofferStrube/Blazor.FileSystemAccess).
This library is inspired by Kristoffer's implementation and project setup.

## License and Note

BSD-3-Clause.

This is a technical showcase, not an official Thinktecture product.
