## Introduction

[![NuGet Downloads (official NuGet)](https://img.shields.io/nuget/dt/PatrickJahr.Blazor.GrpcWeb.DevTools?label=NuGet%20Downloads)](https://www.nuget.org/packages/PatrickJahr.Blazor.GrpcWeb.DevTools/)

gRPC-Web is becoming increasingly popular in the .NET world and, of course, in Blazor.

But not only the technology, also the tooling for gRPC-Web has an important role.
For some time now, there has been a Chrome browser extension for the gRPC-Web Developer Tools.

I have made it my task to make this incredibly useful tool also usable for Blazor :-)

## Getting started

### Prerequisites

You need .NET 7.0 or newer to use this library.

[Download .NET 7](https://dotnet.microsoft.com/download/dotnet/7.0)

You also need the gRPC-Web Developer Tools Chrome Extsion

[Install gRPC-Web Developer Tools](https://chrome.google.com/webstore/detail/grpc-web-developer-tools/kanmilmfkjnoladbbamlclhccicldjaj)

### Platform support

The gRPC-Web Developer Tools only available for Google Chrome and Microsoft Edge browser.

### Installation

You can install the package via NuGet with the Package Manager in your IDE or alternatively using the command line:

```
dotnet add package PatrickJahr.Blazor.GrpcWebDevTools
```

## Usage

The package can be used in Blazor WebAssembly projects.

### Enable gRPC-Web Developer Tools on your project

At first you have to register a `GrpcChannel` in your `ServiceCollection` like this:

```csharp
builder.Services.AddScoped(services =>
{
    var channel = GrpcChannel.ForAddress(backendUrl,
        new GrpcChannelOptions
        {
            HttpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler())
        });

    return channel;
});
```

Then you can add your gRPC-Services with the extension method `AddGrpcService<T>`:

```csharp
builder.Services.AddGrpcService<IConferencesService>();
builder.Services.AddGrpcService<ITimeService>();
```

To enable the gRPC-Web Developer Tools on your project you have two opportunities:

- First you can enable them via the AppSettings:

```json
{
  "GrpcDevToolsEnabled": true
}
```

- Or you can enable them with the extension method `EnableGrpcWebDevTools` in your `Program.cs`:

```
builder.Services.EnableGrpcWebDevTools();
```

## License and Note

BSD-3-Clause.

This is a technical showcase, not an official PatrickJahr product.
