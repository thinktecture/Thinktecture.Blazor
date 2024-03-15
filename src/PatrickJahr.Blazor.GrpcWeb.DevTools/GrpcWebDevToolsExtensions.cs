using Grpc.Core;
using Grpc.Core.Interceptors;
using Grpc.Net.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using ProtoBuf.Grpc.Client;

namespace PatrickJahr.Blazor.GrpcWeb.DevTools
{
    public static class GrpcWebDevToolsExtensions
    {
        private const string GrpcWebDevToolsExtensionName = "__GRPCWEB_DEVTOOLS__";
        private const string GrpcUnaryMethodName = "unary";
        private const string GrpcServerStreamingMethodName = "server_streaming";
        private const string GrpcDevToolsSettingsKey = "GrpcDevToolsEnabled";

        /// <summary>
        /// Registers a gRPC service in the servicecollection.
        /// </summary>
        /// <typeparam name="TService">gRPC Service Type</typeparam>
        /// <param name="services">IServiceCollection</param>

        public static void AddGrpcService<TService>(this IServiceCollection services)
            where TService : class
        {
            services.AddScoped(serviceProvider =>
            {
                var invoker = serviceProvider.GetService<CallInvoker>();
                if (invoker != null)
                    return invoker.CreateGrpcService<TService>();

                try
                {
                    var enabled = serviceProvider.GetRequiredService<IConfiguration>()?.GetValue<bool>(GrpcDevToolsSettingsKey);
                    if (enabled.HasValue && enabled.Value)
                    {
                        if (invoker == null)
                        {
                            var jsRuntime = serviceProvider.GetService<IJSRuntime>();
                            invoker = serviceProvider.GetService<GrpcChannel>().Intercept(new GrpcMessageInterceptor(jsRuntime));
                        }
                        return invoker.CreateGrpcService<TService>();
                    }
                }
                catch (Exception)
                {
                    return GenerateService<TService>(serviceProvider);
                }

                return GenerateService<TService>(serviceProvider);
            });
        }

        /// <summary>
        /// Register a callinvoker, which passes the requests and responses to the gRPC Web Developer Tools.
        /// </summary>
        /// <param name="services">IServiceCollection</param>
        public static void EnableGrpcWebDevTools(this IServiceCollection services)
        {
            // register invoker for dev tools
            services.AddScoped(serviceProvider =>
            {
                var channel = serviceProvider.GetService<GrpcChannel>();
                var jsRuntime = serviceProvider.GetService<IJSRuntime>();
                return channel.Intercept(new GrpcMessageInterceptor(jsRuntime));
            });
        }

        internal static async Task HandleGrpcRequest<TRequest, TResponse>(this IJSRuntime jsRuntime, string method, TRequest request, TResponse response)
        {
            await jsRuntime.InvokeVoidAsync("postMessage", new GrpcDevToolsCall<TRequest, TResponse>(GrpcWebDevToolsExtensionName, method, GrpcUnaryMethodName, request, response));
        }

        internal static async Task HandleGrpcServerStreamRequest<TRequest>(this IJSRuntime jsRuntime, string method, TRequest request)
        {
            await jsRuntime.InvokeVoidAsync("postMessage", new GrpcDevToolsServerRequest<TRequest>(GrpcWebDevToolsExtensionName, method, GrpcServerStreamingMethodName, request));
        }

        internal static async Task HandleGrpcServerStreamResponse<TResponse>(this IJSRuntime jsRuntime, string method, TResponse response)
        {
            await jsRuntime.InvokeVoidAsync("postMessage", new GrpcDevToolsServerResponse<TResponse>(GrpcWebDevToolsExtensionName, method, GrpcServerStreamingMethodName, response));
        }


        private static TService GenerateService<TService>(IServiceProvider serviceProvider)
            where TService : class
        {
            var channel = serviceProvider.GetService<GrpcChannel>();
            return channel.CreateGrpcService<TService>();
        }
    }
}
