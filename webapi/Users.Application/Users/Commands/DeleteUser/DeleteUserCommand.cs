// Application/Users/Commands/DeleteUser/DeleteUserCommand.cs

using MediatR;
using Users.Domain.Abstractions;

namespace Users.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(int Id) : IRequest<Result<object?>>;