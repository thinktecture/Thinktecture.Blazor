using Thinktecture.Blazor.GrpcDevTools.Shared.Services;
using ProtoBuf.Grpc;
using System.Runtime.CompilerServices;

namespace Thinktecture.Blazor.GrpcDevTools.WebApi.Services;

public class TimeService : ITimeService
{
    public IAsyncEnumerable<string> SubscribeAsync(CallContext context = default)
        => SubscribeAsyncImpl(context.CancellationToken);

    private async IAsyncEnumerable<string> SubscribeAsyncImpl(
        [EnumeratorCancellation] CancellationToken cancel)
    {
        while (!cancel.IsCancellationRequested)
        {
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(1), cancel);
            }
            catch (OperationCanceledException)
            {
                break;
            }

            yield return $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}";
        }
    }
}