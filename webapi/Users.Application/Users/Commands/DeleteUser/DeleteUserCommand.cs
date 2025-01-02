using MediatR;

namespace Application.Users.Commands.DeleteUser;

public class DeleteUserCommand : IRequest
{
    public int Id { get; set; }
}
