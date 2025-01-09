using MediatR;
using Application.Common.Models;

namespace Application.Users.Queries
{
    public class GetAllUsersQuery : IRequest<PaginatedList<UserDto>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
