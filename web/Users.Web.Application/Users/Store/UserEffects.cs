using Fluxor;
using Users.Web.Domain.Services;

namespace Users.Web.Application.Users.Store;

public class UserEffects
{
    private readonly IUserService _userService;

    public UserEffects(IUserService userService)
    {
        _userService = userService;
    }

    [EffectMethod(typeof(FetchDataAction))]
    public async Task HandleFetchDataAction(IDispatcher dispatcher)
    {
        var users = await _userService.GetUsersAsync();
        dispatcher.Dispatch(new FetchDataResultAction(users));
    }
}