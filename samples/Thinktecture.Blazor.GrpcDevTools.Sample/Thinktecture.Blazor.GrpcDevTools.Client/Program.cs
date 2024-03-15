using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Thinktecture.Blazor.GrpcDevTools.Client;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using MudBlazor.Services;
using Thinktecture.Blazor.GrpcDevTools.Shared.Services;
using Thinktecture.Blazor.GrpcWeb.DevTools;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var backendUrl = builder.Configuration["Api:BackendUrl"] ?? builder.HostEnvironment.BaseAddress;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(backendUrl) });
builder.Services.AddScoped(services =>
{
    var channel = GrpcChannel.ForAddress(backendUrl, 
        new GrpcChannelOptions 
        { 
            HttpHandler = new GrpcWebHandler(GrpcWebMode.GrpcWeb, new HttpClientHandler()) 
        });
        
    return channel;
});



builder.Services.AddGrpcService<IConferencesService>();
builder.Services.AddGrpcService<ITimeService>();

builder.Services.AddMudServices();

var app = builder.Build();

await app.RunAsync();