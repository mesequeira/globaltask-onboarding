using Microsoft.AspNetCore.Components;
using MudBlazor;
using Users.Web.Application.Users.Validators;
using Users.Web.Domain.Users.Models;

namespace Users.Web.Manegement.Components.Components.Forms;

public partial class EditUserForm
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = null!;

    [Parameter]
    public required UpdateUserModel FormModel { get; set; }

    private EditUserModelValidator _validator = new();
    private MudForm _form = null!;

    private void HandleSubmit()
    {
        _form.Validate();

        if (_form.IsValid)
        {
            MudDialog.Close(DialogResult.Ok(true));
        }
    }
    public static RenderFragment Create(UpdateUserModel model) => builder =>
    {
        builder.OpenComponent<EditUserForm>(0);
        builder.AddAttribute(1, nameof(FormModel), model);
        builder.CloseComponent();
    };

}