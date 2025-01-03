using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Users.Dtos;
using Users.Domain.Abstractions;

namespace Users.Application.Users.Queries
{
    public sealed record GetUserByIdQuery(Guid Id) : IRequest<Result<UserResponseDto>>;
}
