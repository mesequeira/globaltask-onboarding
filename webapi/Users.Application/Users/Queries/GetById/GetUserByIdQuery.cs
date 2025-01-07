using MediatR;
using Users.Application.Users.Dtos;
using Users.Domain.Abstractions;

namespace Users.Application.Users.Queries.GetById;

public sealed record GetUserByIdQuery(Guid Id) : IRequest<Result<UserResponseDto>>;
