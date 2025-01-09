using MediatR;
using Application.Common.Models;

namespace Application.Users.Commands.CreateUser;

public class CreateUserCommand : IRequest<Result<int>>
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime Birthday { get; set; }
}
