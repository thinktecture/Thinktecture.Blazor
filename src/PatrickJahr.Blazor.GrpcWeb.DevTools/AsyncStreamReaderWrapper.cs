using Grpc.Core;
using Microsoft.JSInterop;

namespace PatrickJahr.Blazor.GrpcWeb.DevTools;

public partial class GrpcMessageInterceptor
{
    /// <summary>
    /// StreamReader wrapper, which passes the stream responses to the gRPC Developer Tools.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsyncStreamReaderWrapper<T> : IAsyncStreamReader<T>
    {
        private readonly IAsyncStreamReader<T> _asyncStreamReader;
        private readonly string _methodName = string.Empty;
        private readonly IJSRuntime _jsRuntime;

        public T Current => _asyncStreamReader.Current;

        /// <summary>
        /// Advances the reader to the next element in the sequence, returning the result asynchronously.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token that can be used to cancel the 
        /// operation.</param>
        /// <returns>Task containing the result of the operation: true if the reader was successfully
        /// advanced to the next element; false if the reader has passed the end of the sequence.</returns>
        public async Task<bool> MoveNext(CancellationToken cancellationToken)
        {
            bool result = await _asyncStreamReader.MoveNext(cancellationToken);
            if (result)
            {
                await _jsRuntime.HandleGrpcServerStreamResponse(_methodName, Current);
            }
            return result;
        }

        public AsyncStreamReaderWrapper(IAsyncStreamReader<T> streamReader, string methodName, IJSRuntime jSRuntime)
        {
            _asyncStreamReader = streamReader;
            _methodName = methodName;
            _jsRuntime = jSRuntime;
        }
    }
}