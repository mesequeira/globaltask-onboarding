using Fluxor;
using Users.Web.Domain.DTO;

namespace Users.Web.Application.Users;

[FeatureState]
public class UserState
{
    public bool IsLoading { get; init; }
    public List<UserDTO> Users { get; init; } = new();

    public UserState()
    {
    }

    public UserState(List<UserDTO> users)
    {
        Users = users;
        IsLoading = false;
    }

    public UserState(bool isLoading, List<UserDTO>? users)
    {
        Users = users ?? new();
        IsLoading = isLoading;
    }
}