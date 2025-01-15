using Microsoft.AspNetCore.Components;
using MudBlazor;
using Users.Web.Application.Users.Validators;
using Users.Web.Domain.Users.Models;

namespace Users.Web.Manegement.Components.Components.Forms;

public partial class CreateUserForm
{
    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; } = null!;

    [Parameter]
    public required CreateUserModel FormModel { get; set; }

    private CreateUserModelValidator _validator = new();

    private MudForm _form = null!;
    private void HandleSubmit()
    {
        _form.Validate();

        if (_form.IsValid)
        {
            MudDialog.Close(DialogResult.Ok(true));
        }
    }

    public static RenderFragment Create(CreateUserModel model) => builder =>
    {
        builder.OpenComponent<CreateUserForm>(0);
        builder.AddAttribute(1, nameof(FormModel), model);
        builder.CloseComponent();
    };
}