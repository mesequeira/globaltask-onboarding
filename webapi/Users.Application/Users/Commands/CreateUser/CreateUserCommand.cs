// Application/Users/Commands/CreateUser/CreateUserCommand.cs

using MediatR;

namespace Users.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    int Id,
    string Name,
    string Email,
    string PhoneNumber,
    DateTime BirthDate
) : IRequest<int>;