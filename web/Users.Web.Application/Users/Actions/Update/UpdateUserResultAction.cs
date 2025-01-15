using Users.Web.Domain.Users;

namespace Users.Web.Application.Users.Actions.Update;

public sealed record UpdateUserResultAction(Guid Id, string Email, string PhoneNumber);
