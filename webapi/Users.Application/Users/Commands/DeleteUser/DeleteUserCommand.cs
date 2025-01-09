using MediatR;
using Application.Common.Models;

namespace Application.Users.Commands.DeleteUser;

public class DeleteUserCommand : IRequest<Result<Unit>>
{
    public int Id { get; set; }
}
