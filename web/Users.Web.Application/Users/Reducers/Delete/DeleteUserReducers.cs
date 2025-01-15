using Fluxor;
using Users.Web.Application.Users.Actions.Delete;
using Users.Web.Application.Users.States;
using Users.Web.Domain.Users;

namespace Users.Web.Application.Users.Reducers.Delete;

public static class DeleteUserReducers
{
    [ReducerMethod(typeof(DeleteUserAction))]
    public static UsersState ReduceDeleteUserAction(UsersState state) =>
      new UsersState(
        isLoading: true,
        users: state.Users
      );

    [ReducerMethod]
    public static UsersState ReduceDeleteUserResultAction(UsersState state, DeleteUserResultAction action)
    {
        var updatedUsers = state.Users?.Where(user => user.Id != action.Id) ?? new List<UserResponseDto>();

        return new UsersState(
            isLoading: false,
            users: updatedUsers
        );
    }
}
