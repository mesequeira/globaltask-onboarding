using Fluxor;
using Users.Web.Application.Abstract;
using Users.Web.Application.Users.Actions.Get;
using Users.Web.Application.Users.States;

namespace Users.Web.Application.Users.Reducers.Get;

public static class FetchUserReducers
{
    [ReducerMethod(typeof(FetchUsersAction))]
    public static UsersState ReduceFetchUsersAction(UsersState state) =>
        new UsersState(
          isLoading: true,
          users: null
        );

    [ReducerMethod]
    public static UsersState ReduceFetchDataResultAction(UsersState state, FetchUsersResultAction action) =>
      new UsersState(
        isLoading: false,
        users: action.Users
      );
}
