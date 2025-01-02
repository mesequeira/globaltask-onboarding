// Application/Users/Queries/GetAllUsers/GetAllUsersQuery.cs

using MediatR;
using Users.Domain.Models;

namespace Users.Application.Users.Queries.GetAllUsers;

public record GetAllUsersQuery() : IRequest<IEnumerable<User>>;