# Thinktecture.Blazor.FileHandling

[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/Thinktecture.Blazor.FileHandling?label=NuGet%20Downloads)](https://www.nuget.org/packages/Thinktecture.Blazor.FileHandling/)

## Introduction

A Blazor wrapper for the [File Handling API](https://wicg.github.io/manifest-incubations/#file_handlers-member).

The File Handling API allows you to register your Progressive Web App as a file handler for certain file types.

## Getting started

### Prerequisites

You need .NET 6.0 or newer to use this library.

[Download .NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)

### Platform support

[Platform support for Badging API](https://caniuse.com/mdn-api_launchqueue)

### Installation

You can install the package via NuGet with the Package Manager in your IDE or alternatively using the command line:

```
dotnet add package Thinktecture.Blazor.FileHandling
```

## Usage

The package can be used in Blazor WebAssembly projects.

### Add to service collection

To make the FileHandlingService available on all pages, register it at the IServiceCollection in `Program.cs` before the host is built:

```csharp
builder.Services.AddFileHandlingService();
```

### Checking for browser support

Before using the File Handling API, you should first test if the API is supported on the target platform by calling the `IsSupportedAsync()` method.
This method returns a boolean to indicate whether the File Handling API is supported or not.

```csharp
var isSupported = await fileHandlingService.IsSupportedAsync();
if (isSupported)
{
    // enable file handling feature
}
else
{
    // use fallback mechanism or hide/disable feature
}
```

Internally, this method tests for the presence of the `launchQueue` property on the `window` object of the target browser.

### Register in Web App Manifest

The API consists of two parts:
First, you need to declare support for the target file types in your Web Application Manifest (typically called _manifest.json_ or _manifest.webmanifest_).
The property `file_handlers` contains an array of file handlers.
Each file handler consists of the list of accepted media types and file extensions (`accept` property).
The `action` property contains the address that should be launched when a file with the particular media type or file extension was opened.

```json
{
  "file_handlers": [{
    "action": "./",
    "accept": {
      "text/plain": [".txt"]
    }
  }]
}
```

During installation, the application is registered at the target platform as a handler for the given file types.

### Access launch parameters during runtime

To access the launch parameters during runtime, call the `SetConsumerAsync()` method.
This method takes an action that is immediately called with the `LaunchParams`.
This object has a `Files` property that currently directly contains a list of `FileSystemHandle`s for the files.
The handles are either an instance of `FileSystemFileHandle` or `FileSystemDirectoryHandle`, so make sure to check for the correct type.
To get the binary contents of files, call the `GetFileAsync()` method on a `FileSystemFileHandle`.
This method returns a `File` object that offers methods like `TextAsync()` and `ArrayBufferAsync()` to retrieve the file's contents in different formats.

```csharp
var isSupported = await fileHandlingService.IsSupportedAsync();
if (isSupported)
{
    await _fileHandlingService.SetConsumerAsync(async (launchParams) =>
    {
        foreach (var fileSystemHandle in launchParams.Files)
        {
            if (fileSystemHandle is FileSystemFileHandle fileSystemFileHandle)
            {
                var file = await fileSystemFileHandle.GetFileAsync();
    
                var text1 = await file.TextAsync();
                Console.WriteLine(text1);
    
                var bytes = await file.ArrayBufferAsync();
                var text2 = System.Text.Encoding.UTF8.GetString(bytes);
                Console.WriteLine(text2);
            }
        }
    });
}
```

## Related articles

- [WICG Specification (`file_handlers` member)](https://wicg.github.io/manifest-incubations/#file_handlers-member)
- [WICG Specification (launch queue)](https://wicg.github.io/manifest-incubations/#launch-queue-and-launch-params)
- [Blog post on web.dev](https://web.dev/file-handling/)
- [Browser support on caniuse.com](https://caniuse.com/mdn-api_launchqueue)

## Acknowledgements

Thanks to [Kristoffer Strube](https://twitter.com/kstrubeg) who provides [a Blazor wrapper for the File System Access API](https://github.com/KristofferStrube/Blazor.FileSystemAccess).
This library is inspired by Kristoffer's implementation and project setup, and uses the same classes to access file contents.

## License and Note

BSD-3-Clause.

This is a technical showcase, not an official Thinktecture product.
