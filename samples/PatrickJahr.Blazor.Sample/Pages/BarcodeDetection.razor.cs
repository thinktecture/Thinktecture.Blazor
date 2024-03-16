using Microsoft.AspNetCore.Components;
using PatrickJahr.Blazor.BarcodeDetection;

namespace PatrickJahr.Blazor.Sample.Pages;
public partial class BarcodeDetection {
    [Inject] private BarcodeDetectionService _barcodeDetectionService { get; set; } = default!;

    private bool _isSupported;
    private string[] _barcodes = Array.Empty<string>();

    protected override async Task OnInitializedAsync()
    {
        _isSupported = await _barcodeDetectionService.IsSupportedAsync();
        if (_isSupported) {
            _barcodes = await _barcodeDetectionService.GetSupportedFormatsAsync();
        }
        await base.OnInitializedAsync();
    }
}