using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Users.Web.Manegement.Components.Components.Dialogs;

public partial class ConfirmDialog
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = null!;

    [Parameter]
    public string ContentText { get; set; } = string.Empty;

    [Parameter]
    public string ButtonText { get; set; } = string.Empty;

    [Parameter]
    public Color Color { get; set; }

    private void Submit() => MudDialog.Close(DialogResult.Ok(true));

    private void Cancel() => MudDialog.Cancel();
}