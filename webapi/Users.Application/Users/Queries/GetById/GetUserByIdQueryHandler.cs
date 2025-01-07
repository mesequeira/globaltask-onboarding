using Mapster;
using MediatR;
using Users.Application.Users.Dtos;
using Users.Domain.Abstractions;
using Users.Domain.Users.Abstractions;
using Users.Domain.Users.Errors;
using Users.Domain.Users.Models;

namespace Users.Application.Users.Queries.GetById;

public sealed class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, Result<UserResponseDto>>
{
    public async Task<Result<UserResponseDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetByIdAsync(request.Id, cancellationToken);

        if (user == null)
        {
            return Result<UserResponseDto>.Failure([UserErrors.NotFound(request.Id)], statusCode: 204);
        }

        UserResponseDto userResponse = user.Adapt<UserResponseDto>();

        return Result<UserResponseDto>.Success(userResponse, statusCode: 200);
    }
}
