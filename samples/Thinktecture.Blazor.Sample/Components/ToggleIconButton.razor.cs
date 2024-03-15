using Microsoft.AspNetCore.Components;

namespace Thinktecture.Blazor.Sample.Components;

public partial class ToggleIconButton
{
    [Parameter, EditorRequired] public string Icon { get; set; } = string.Empty;
    [Parameter, EditorRequired] public string ToggledIcon { get; set; } = string.Empty;
    [Parameter] public string Class { get; set; } = string.Empty;

    [Parameter] public bool Toggled { get; set; }
    [Parameter] public EventCallback<bool> ToggledChanged { get; set; }

    
    private async Task OnClicked()
    {
        Toggled = !Toggled;
        await ToggledChanged.InvokeAsync(Toggled);
    }
}