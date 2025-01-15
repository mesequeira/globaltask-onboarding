using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Users.Web.Application.Users.Actions.Create;
using Users.Web.Application.Users.Actions.Delete;
using Users.Web.Application.Users.Actions.Get;
using Users.Web.Application.Users.Actions.Update;
using Users.Web.Application.Users.States;
using Users.Web.Domain.Users;
using Users.Web.Domain.Users.Models;
using Users.Web.Infrastructure.Refit.Users;
using Users.Web.Manegement.Components.Components.Dialogs;
using Users.Web.Manegement.Components.Components.Forms;

namespace Users.Web.Manegement.Components.Pages;

public partial class UsersPage : FluxorComponent
{
    [Inject]
    public required IRefitUsersApi _usersApi { get; set; }

    [Inject]
    public required IDialogService _dialogService { get; set; }

    [Inject]
    public required IState<UsersState> State { get; set; }

    [Inject]
    public required IDispatcher Dispatcher { get; set; }

    private MudTooltip _toolTip = null!;
    private async Task HandleDelete(Guid id)
    {
        DialogParameters<ConfirmDialog> parameters = new()
        {
            { x => x.ContentText, "Está seguro que quiere eliminar este usuario? Este proceso no se puede revertir." },
            { x => x.ButtonText, "Eliminar" },
            { x => x.Color, Color.Error }
        };

        DialogOptions options = new() { CloseButton = true, MaxWidth = MaxWidth.ExtraSmall };

        IDialogReference dialogReference = await _dialogService.ShowAsync<ConfirmDialog>("Eliminar", parameters, options);

        DialogResult? dialogResult = await dialogReference.Result;

        if (
            dialogResult != null 
            && dialogResult.Data != null 
            && (bool)dialogResult.Data
        )
        {
            _toolTip.Visible = false;

            DeleteUserAction action = new(id, "Custom reason");
            Dispatcher.Dispatch(action);
        }
    }

    private async Task HandleCrate() 
    {
        CreateUserModel userModel = new();

        DialogParameters<FormDialog> parameters = new()
        {
            { nameof(FormDialog.Form), CreateUserForm.Create(userModel) }
        };

        DialogOptions options = new() { CloseButton = true, FullWidth = true ,MaxWidth = MaxWidth.Medium };

        IDialogReference dialogReference = await _dialogService.ShowAsync<FormDialog>("Crear usuario", parameters, options);

        DialogResult? dialogResult = await dialogReference.Result;

        if (
            dialogResult != null
            && dialogResult.Data != null
            && (bool)dialogResult.Data
        )
        {
            Dispatcher.Dispatch(new CreateUserAction(userModel));
        }
        
    }
    private async Task HandleUpdate(Guid id)
    {
        UserResponseDto user = State.Value.Users.FirstOrDefault(u => u.Id == id)!;
        UpdateUserModel userModel = new();

        userModel.Email = user.Email;
        userModel.PhoneNumber = user.PhoneNumber;

        DialogParameters<FormDialog> parameters = new()
        {
            { nameof(FormDialog.Form), EditUserForm.Create(userModel) }
        };

        DialogOptions options = new() { CloseButton = true, FullWidth = true, MaxWidth = MaxWidth.Medium };

        IDialogReference dialogReference = await _dialogService.ShowAsync<FormDialog>("Editar usuario", parameters, options);

        DialogResult? dialogResult = await dialogReference.Result;

        if (
            dialogResult != null
            && dialogResult.Data != null
            && (bool)dialogResult.Data
        )
        {

            UpdateUserAction action = new(id, userModel);
            Dispatcher.Dispatch(action);
        }
    }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Dispatcher.Dispatch(new FetchUsersAction());
    }
}