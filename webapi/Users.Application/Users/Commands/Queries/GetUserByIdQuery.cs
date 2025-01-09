using MediatR;
using Application.Common.Models;
using Application.Users.Queries;

namespace Users.Application.Users.Commands.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public int Id { get; set; }
    }
}
