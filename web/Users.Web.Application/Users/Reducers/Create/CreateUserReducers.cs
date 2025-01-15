using Fluxor;
using Users.Web.Application.Users.Actions.Create;
using Users.Web.Application.Users.States;

namespace Users.Web.Application.Users.Reducers.Create;

public static class CreateUserReducers
{
    [ReducerMethod(typeof(CreateUserAction))]
    public static UsersState ReduceCreateUserAction(UsersState state) =>
       new UsersState(
         isLoading: true,
         users: state.Users
       );

    [ReducerMethod]
    public static UsersState ReduceCreateUserResultAction(UsersState state, CreateUserResultAction action) =>
      new UsersState(
        isLoading: false,
        users: state.Users!.Prepend(action.User).ToList()
      );
}
