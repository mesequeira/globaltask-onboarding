// Application/Users/Queries/GetAllUsers/GetAllUsersQuery.cs

using MediatR;
using Users.Application.DTOs.Users;
using Users.Domain.Abstractions;
using Users.Domain.Models;

namespace Users.Application.Users.Queries.GetAllUsers;

public record GetAllUsersQuery(int page, int size, string sortBy) : IRequest<Result<PaginatedUsersDTO>>;
//public record GetAllUsersQuery() : IRequest<IEnumerable<User>>;
