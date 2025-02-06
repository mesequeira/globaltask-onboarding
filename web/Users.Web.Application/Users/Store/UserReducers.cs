using Fluxor;
using Users.Web.Domain.DTO;
using Users.Web.Domain.Services;

namespace Users.Web.Application.Users.Store;

public static class UserReducers
{
    [ReducerMethod]
    public static UserState ReduceGetUsers(UserState state, GetUsersAction action) =>
        // new UserState(IUserService.GetUsersAsync());
        new UserState(new List<UserDTO>(){new UserDTO("Carlos", "Car@gmail.com", "123456789", new DateTime(1980,12,12))});

    [ReducerMethod]
    public static UserState ReduceFetchData(UserState state, FetchDataAction action) =>
        // new UserState(IUserService.GetUsersAsync());
        new UserState(true, null);
    
    [ReducerMethod]
    public static UserState ReduceFetchDataResultAction (UserState state, FetchDataResultAction action) =>
        // new UserState(IUserService.GetUsersAsync());
        new UserState(false, action.Users);
    
}