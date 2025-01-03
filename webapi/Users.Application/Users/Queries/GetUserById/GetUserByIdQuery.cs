using MediatR;
using Users.Domain.Models;

namespace Users.Application.Users.Queries.GetUserById;

public record GetUserByIdQuery(int Id) : IRequest<User?>;