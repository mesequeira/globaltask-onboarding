using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Users.Dtos;
using Users.Application.Users.Queries;
using Users.Domain.Abstractions;
using Users.Domain.Users.Abstractions;
using Users.Domain.Users.Errors;
using Users.Domain.Users.Models;

namespace Users.Application.Users.Handlers
{
    public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<UserResponseDto>>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<Result<UserResponseDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            User? user = await _userRepository.GetByIdAsync(request.Id); 

            if(user == null)
            {
                return Result<UserResponseDto>.Failure([UserErrors.NotFound(request.Id)], statusCode: 204);
            }

            UserResponseDto userResponse = user.Adapt<UserResponseDto>();

            return Result<UserResponseDto>.Success(userResponse, statusCode: 200);
        }
    }
}
