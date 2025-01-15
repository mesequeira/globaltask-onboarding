using Fluxor;
using Users.Web.Application.Users.Actions.Create;
using Users.Web.Application.Users.Actions.Update;
using Users.Web.Application.Users.States;
using Users.Web.Domain.Users;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Users.Web.Application.Users.Reducers.Update;

public static class UpdateUserReducers
{
    [ReducerMethod(typeof(UpdateUserAction))]
    public static UsersState ReduceUpdateUserAction(UsersState state) =>
      new UsersState(
        isLoading: true,
        users: state.Users
      );

    [ReducerMethod]
    public static UsersState ReduceUpdateUserResultAction(UsersState state, UpdateUserResultAction action)
    {
        var updatedUsers = state.Users?.Select(user =>
        {
            if (user.Id == action.Id)
            {
                return new UserResponseDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    Email = action.Email,
                    PhoneNumber = action.PhoneNumber,
                    Birthday = user.Birthday
                };
            }
            return user;
        }).ToList() ?? new List<UserResponseDto>();

        return new UsersState(
            isLoading: false,
            users: updatedUsers
        );
    }
}
