using MediatR;
using Application.Common.Models;

namespace Application.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<Result<Unit>>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? Birthday { get; set; }
}
