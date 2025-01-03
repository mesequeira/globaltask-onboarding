using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Users.Models;

namespace Users.Domain.Users.Abstractions
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(Expression<Func<User, object>> sortExpression, int page, int size);
        Task<User?> GetByIdAsync(Guid id);
        void Insert(User user);
        void Update(User user);
        void Delete(User user);
    }
}
