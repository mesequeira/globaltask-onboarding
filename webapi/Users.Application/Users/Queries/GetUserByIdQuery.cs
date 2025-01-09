using MediatR;
using Application.Common.Models;

namespace Application.Users.Queries
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public int Id { get; set; }
    }
}
