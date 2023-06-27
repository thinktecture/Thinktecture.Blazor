using Microsoft.AspNetCore.Components;

namespace Thinktecture.Blazor.Sample.Components;

public partial class IconButton
{
    [Parameter] public string Icon { get; set; } = string.Empty;
    [Parameter] public string Class { get; set; } = string.Empty;

    [Parameter] public EventCallback Clicked { get; set; }

    private async Task OnClicked()
    {
        await Clicked.InvokeAsync();
    }
}