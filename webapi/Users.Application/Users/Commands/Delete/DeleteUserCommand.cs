using MediatR;
using Users.Domain.Abstractions;

namespace Users.Application.Users.Commands.Delete;

public sealed record DeleteUserCommand(Guid Id, string Reason) : IRequest<Result>;
