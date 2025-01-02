// Application/Users/Commands/UpdateUser/UpdateUserCommand.cs

using MediatR;

namespace Users.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    Guid Id,
    string Name,
    string Email,
    string PhoneNumber,
    DateTime BirthDate
) : IRequest<bool>;