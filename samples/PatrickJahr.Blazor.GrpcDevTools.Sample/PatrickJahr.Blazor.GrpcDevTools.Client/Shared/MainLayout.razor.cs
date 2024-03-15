using Microsoft.AspNetCore.Components;
using Grpc.Core;
using ProtoBuf.Grpc;
using MudBlazor;
using PatrickJahr.Blazor.GrpcDevTools.Shared.Services;

namespace PatrickJahr.Blazor.GrpcDevTools.Client.Shared
{
    public partial class MainLayout : IDisposable
    {
        [Inject] private ITimeService _timeService { get; set; } = default!;

        private string _time = "";
        private CancellationTokenSource? _cts;

        private static readonly MudTheme DefaultTheme = new()
        {
            Palette = new Palette
            {
                Black = "#272c34",
                AppbarBackground = "#ffffff",
                AppbarText = "#ff584f",
                DrawerBackground = "#ff584f",
                DrawerText = "ffffff",
                DrawerIcon = "ffffff",
                Primary = "#ff584f",
                Secondary = "#3d6fb4"
            },
            Typography = new Typography()
            {
                Default = new Default()
                {
                    FontFamily = new[] { "Montserrat", "Helvetica", "Arial", "sans-serif" }
                }
            }
        };

        protected override async Task OnInitializedAsync()
        {
            await StartTime();
            await base.OnInitializedAsync();
        }

        private async Task StartTime()
        {
            if (_timeService == null)
            {
                return;
            }

            _cts = new CancellationTokenSource();
            var options = new CallOptions(cancellationToken: _cts.Token);

            try
            {
                await foreach (var time in _timeService.SubscribeAsync(new CallContext(options)))
                {
                    _time = time;
                    StateHasChanged();
                }
            }
            catch (RpcException)
            {
            }
            catch (OperationCanceledException)
            {
            }
        }
        private void StopTime()
        {
            _cts?.Cancel();
            _cts = null;
            _time = "";
        }

        public void Dispose()
        {
            StopTime();
        }
    }
}