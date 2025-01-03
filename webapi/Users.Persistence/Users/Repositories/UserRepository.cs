using Azure.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Users.Dtos;
using Users.Domain.Abstractions;
using Users.Domain.Users.Abstractions;
using Users.Domain.Users.Errors;
using Users.Domain.Users.Models;

namespace Users.Persistence.Users.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        public UserRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }

        public Task<User?> GetByIdAsync(Guid id) 
            => _context.Users.SingleOrDefaultAsync(u => u.Id == id);

        public void Insert(User user) => _context.Users.Add(user);
        public void Update(User user) => _context.Users.Update(user);
        public void Delete(User user) => _context.Users.Remove(user);
        public async Task<List<User>> GetAllAsync(Expression<Func<User, object>> sortExpression, int page, int size)
        {
            IQueryable<User> usersQuery = _context.Users.AsQueryable();

            usersQuery = usersQuery.OrderBy(sortExpression);
            usersQuery = usersQuery.Skip((page - 1) * size).Take(size);

            List<User> users = await usersQuery.ToListAsync();

            return users;
        }
    }
}
