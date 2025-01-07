using MediatR;
using Users.Application.Users.Dtos;
using Users.Domain.Abstractions;

namespace Users.Application.Users.Queries.GetAll;

public sealed record GetUsersQuery(
    int Page,
    int Size,
    string SortBy) : IRequest<Result<PaginatedUserDto>>;
