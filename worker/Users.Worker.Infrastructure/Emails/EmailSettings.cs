using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Worker.Infrastructure.Emails
{
    public sealed record EmailSettings(string Email,
        string Password,
        int Port,
        string Host);
}
