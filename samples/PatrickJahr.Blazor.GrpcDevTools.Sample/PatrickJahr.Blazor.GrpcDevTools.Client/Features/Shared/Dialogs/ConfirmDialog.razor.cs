using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace PatrickJahr.Blazor.GrpcDevTools.Client.Features.Shared.Dialogs
{
    public partial class ConfirmDialog
    {
        [CascadingParameter] MudDialogInstance MudDialog { get; set; } = default!;

        [Parameter] public string ContentText { get; set; } = string.Empty;

        [Parameter] public string ButtonText { get; set; } = "Ok";

        [Parameter] public Color Color { get; set; } = Color.Default;

        void Submit() => MudDialog.Close(DialogResult.Ok(true));
        void Cancel() => MudDialog.Cancel();
    }
}