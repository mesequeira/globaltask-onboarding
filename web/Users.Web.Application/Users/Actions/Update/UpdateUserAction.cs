using Users.Web.Domain.Users.Models;

namespace Users.Web.Application.Users.Actions.Update;

public sealed record UpdateUserAction(Guid Id, UpdateUserModel UpdateUserModel);
