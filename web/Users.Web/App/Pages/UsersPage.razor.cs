using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Users.Web.App.Components;
using Users.Web.Application.Users;
using Users.Web.Application.Users.Store;
using Users.Web.Domain.DTO;

namespace Users.Web.App.Pages;

public partial class UsersPage
{
    [Inject] private IState<UserState> UserState { get; set; }
    [Inject] public IDispatcher Dispatcher { get; set; }
    
    private List<UserDTO>? users;

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        
        // var action = new GetUsersAction();
        // Dispatcher.Dispatch(action);
        Dispatcher.Dispatch(new FetchDataAction());

        // Dispatcher.Dispatch(new Users.Web.Application.Users.LoadUsersAction());
        users = UserState.Value.Users;
        // users = await UserService.GetUsersAsync(page: 1, size: 25, sortBy: "name");
    }

    private async Task CrearUsuario()
    {
        var parameters = new DialogParameters { ["User"] = null, ["OnEdit"] = false };
        var options = new DialogOptions { CloseOnEscapeKey = true };
        var dialog = DialogService.Show<UserEditCreateModal>("Editar Usuario", parameters, options);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            users = await UserService.GetUsersAsync(page: 1, size: 25, sortBy: "name");
        }
    }

    private async Task EditarUsuario(UserDTO usuario)
    {
        var parameters = new DialogParameters { ["User"] = usuario, ["OnEdit"] = true };
        var options = new DialogOptions { CloseOnEscapeKey = true };
        var dialog = DialogService.Show<UserEditCreateModal>("Editar Usuario", parameters, options);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            users = await UserService.GetUsersAsync(page: 1, size: 25, sortBy: "name");
        }
    }

    private async Task EliminarUsuario(UserDTO user)
    {
        var parameters = new DialogParameters { ["User"] = user };
        var options = new DialogOptions { CloseOnEscapeKey = true };
        var dialog = DialogService.Show<UserDeleteModal>("Eliminar Usuario", parameters, options);
        var result = await dialog.Result;

        if (!result.Canceled)
        {
            await UserService.DeleteUserAsync(user.Id);
            users = await UserService.GetUsersAsync(page: 1, size: 25, sortBy: "name");
        }
    }
}