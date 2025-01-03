// Application/Users/Commands/UpdateUser/UpdateUserCommand.cs

using MediatR;
using Users.Domain.Abstractions;

namespace Users.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand(
    int Id,
    string Name,
    string Email,
    string PhoneNumber,
    DateTime BirthDate
) : IRequest<Result<int>>;