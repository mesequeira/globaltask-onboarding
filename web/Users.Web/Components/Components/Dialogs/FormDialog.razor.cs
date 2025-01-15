using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace Users.Web.Manegement.Components.Components.Dialogs;

public partial class FormDialog
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = null!;

    [Parameter]
    public required RenderFragment Form { get; set; } = null!;

    [Parameter]
    public string? CssClass { get; set; }
}