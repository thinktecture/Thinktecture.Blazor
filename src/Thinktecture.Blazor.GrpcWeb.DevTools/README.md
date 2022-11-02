## Introduction



## Getting started

### Prerequisites

You need .NET 6.0 or newer to use this library.

[Download .NET 6](https://dotnet.microsoft.com/download/dotnet/6.0)

You also need the gRPC-Web Developer Tools Chrome Extsion
[Install gRPC-Web Developer Tools](https://chrome.google.com/webstore/detail/grpc-web-developer-tools/kanmilmfkjnoladbbamlclhccicldjaj)

### Installation

You can install the package via NuGet with the Package Manager in your IDE or alternatively using the command line:

```
dotnet add package Thinktecture.Blazor.GrpcWebDevTools
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


