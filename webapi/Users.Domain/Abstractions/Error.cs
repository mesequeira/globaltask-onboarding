using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Users.Domain.Abstractions
{
    public class Error
    {
        public Error(string code, string? description = null) 
        {
            Code = code;
            Description = description;
        }
        public string Code { get; set; } = null!;
        public string? Description { get; set; } = null!;

        public static readonly Error None = new(string.Empty);
    }
}
