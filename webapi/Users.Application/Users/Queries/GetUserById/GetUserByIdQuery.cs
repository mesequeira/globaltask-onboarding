using MediatR;
using Users.Domain.Models;

namespace Users.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(Guid Id) : IRequest<User?>;