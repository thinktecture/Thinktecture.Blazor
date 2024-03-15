using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using PatrickJahr.Blazor.AsyncClipboard;
using PatrickJahr.Blazor.Badging;
using PatrickJahr.Blazor.FileHandling;
using PatrickJahr.Blazor.PwaUpdate;
using PatrickJahr.Blazor.Sample;
using PatrickJahr.Blazor.ScreenWakeLock;
using PatrickJahr.Blazor.ViewTransitions;
using PatrickJahr.Blazor.WebShare;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(typeof(App).Assembly);
    options.UseReduxDevTools();
});
builder.Services.AddAsyncClipboardService();
builder.Services.AddBadgingService();
builder.Services.AddFileHandlingService();
builder.Services.AddWebShareService();
builder.Services.AddUpdateService();
builder.Services.AddScreenWakeLockService();
builder.Services.AddViewTransitionService();

await builder.Build().RunAsync();
