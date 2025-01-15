using Users.Web.Domain.Users;

namespace Users.Web.Application.Users.Actions.Get;

public sealed record FetchUsersResultAction(IEnumerable<UserResponseDto> Users);
