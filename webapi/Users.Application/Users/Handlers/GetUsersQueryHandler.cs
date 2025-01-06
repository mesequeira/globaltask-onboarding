using Mapster;
using MediatR;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
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
    public sealed class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, Result<PaginatedUserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<PaginatedUserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var propertyInfo = typeof(User).GetProperty(request.SortBy);

            if (propertyInfo == null)
            {
                return Result<PaginatedUserDto>.Failure([UserErrors.SortByPropertyNotFound(request.SortBy)], statusCode: 400);
            }

            Expression<Func<User, object>> sortExpression = GetSortProperty(request.SortBy);

            List<User> users = await _userRepository.GetAllAsync(sortExpression, request.Page, request.Size);

            List<UserResponseDto> usersDto = users.Adapt<List<UserResponseDto>>();

            PaginatedUserDto response = new(usersDto.Count, request.Size, request.Page, usersDto);

            return Result<PaginatedUserDto>.Success(response, statusCode: 200);
        }

        private static Expression<Func<User, object>> GetSortProperty(string sortyBy)
        {
            Expression<Func<User, object>> sortByKey = sortyBy switch
            {
                "UserName" => user => user.UserName,
                "PhoneNumber" => user => user.PhoneNumber,
                "Birthday" => user => user.Birthday,
                "Email" => user => user.Email,
                _ => user => user.Id
            };

            return sortByKey;
        }
    }
}
