using Fluxor;
using Users.Web.Domain.Users;

namespace Users.Web.Application.Users.States;

[FeatureState]
public sealed class UsersState
{
    public bool IsLoading { get; }
    public IEnumerable<UserResponseDto>? Users { get; }
    private UsersState() { }

    public UsersState(bool isLoading, IEnumerable<UserResponseDto>? users)
    {
        IsLoading = isLoading;
        Users = users ?? Array.Empty<UserResponseDto>();
    }
}
