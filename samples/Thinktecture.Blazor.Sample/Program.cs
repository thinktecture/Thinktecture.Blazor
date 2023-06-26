using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Thinktecture.Blazor.AsyncClipboard;
using Thinktecture.Blazor.Badging;
using Thinktecture.Blazor.FileHandling;
using Thinktecture.Blazor.PwaUpdate;
using Thinktecture.Blazor.Sample;
using Thinktecture.Blazor.ScreenWakeLock;
using Thinktecture.Blazor.WebShare;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddAsyncClipboardService();
builder.Services.AddBadgingService();
builder.Services.AddFileHandlingService();
builder.Services.AddWebShareService();
builder.Services.AddUpdateService();
builder.Services.AddScreenWakeLockService();

await builder.Build().RunAsync();
