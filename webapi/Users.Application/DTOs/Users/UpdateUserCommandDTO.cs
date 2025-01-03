using MediatR;
using Users.Domain.Abstractions;

namespace Users.Application.DTOs.Users;

public record UpdateUserCommandDTO(
    string Name,
    string Email,
    string PhoneNumber,
    DateTime BirthDate
) : IRequest<Result<int>>;