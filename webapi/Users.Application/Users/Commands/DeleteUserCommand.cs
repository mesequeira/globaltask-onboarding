using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Abstractions;

namespace Users.Application.Users.Commands
{
    public sealed record DeleteUserCommand(Guid Id) : IRequest<Result>;
}
