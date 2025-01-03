using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Users.Dtos;
using Users.Domain.Abstractions;

namespace Users.Application.Users.Commands
{
    public sealed record CreateUserCommand(string UserName, 
        string Email, 
        string Password, 
        string PhoneNumber,
        DateTime Birthday
    ) 
    : IRequest<Result<Guid>>;
}
