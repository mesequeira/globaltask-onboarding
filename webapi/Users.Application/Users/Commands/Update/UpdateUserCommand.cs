using MediatR;
using Users.Domain.Abstractions;

namespace Users.Application.Users.Commands.Update;

public sealed record UpdateUserCommand(
    Guid Id,
    string Password,
    string PhoneNumber,
    string Email) : IRequest<Result>;
