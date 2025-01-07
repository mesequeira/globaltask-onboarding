using MediatR;
using Users.Domain.Abstractions;

namespace Users.Application.Users.Commands.Create;

public sealed record CreateUserCommand(string UserName,
    string Email,
    string Password,
    string PhoneNumber,
    DateTime Birthday
)
: IRequest<Result<Guid>>;
