﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Abstractions;

namespace Users.Domain.Users.Errors
{
    public static class UserErrors
    {
        public static Error NotFound(Guid id) 
            => new(UserErrorCodes.NotFound, $"User with id {id} was not found.");
        public static Error SortByPropertyNotFound(string sortBy) 
            => new(UserErrorCodes.SortByPropertyNotFound, $"The property {sortBy} was not found.");
    }
}
