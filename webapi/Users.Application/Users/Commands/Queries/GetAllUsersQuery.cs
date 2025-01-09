using MediatR;
using Application.Common.Models;
using Application.Users.Queries;

namespace Users.Application.Users.Commands.Queries
{
    public class GetAllUsersQuery : IRequest<PaginatedList<UserDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
