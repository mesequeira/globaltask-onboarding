// Application/Users/Commands/DeleteUser/DeleteUserCommand.cs

using MediatR;

namespace Users.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid Id) : IRequest<bool>;