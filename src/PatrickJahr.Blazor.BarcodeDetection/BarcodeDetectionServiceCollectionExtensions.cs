using Microsoft.Extensions.DependencyInjection;

namespace PatrickJahr.Blazor.BarcodeDetection;

public static class BarcodeDetectionCollectionExtensions
{
    public static IServiceCollection AddBarcodeDetectionService(this IServiceCollection services) => services.AddScoped<BarcodeDetectionService>();
}
