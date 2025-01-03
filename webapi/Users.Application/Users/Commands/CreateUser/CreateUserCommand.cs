// Application/Users/Commands/CreateUser/CreateUserCommand.cs

using MediatR;
using Users.Domain.Abstractions;

namespace Users.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string Name,
    string Email,
    string PhoneNumber,
    DateTime BirthDate
) : IRequest<Result<int>>;