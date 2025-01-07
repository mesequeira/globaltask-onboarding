using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Users.Domain.Users.Abstractions;
using Users.Domain.Users.Models;

namespace Users.Persistence.Users.Repositories;

public sealed class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) 
        => context.Users.SingleOrDefaultAsync(u => u.Id == id, cancellationToken);

    public void Insert(User user) => context.Users.Add(user);
    public void Update(User user) => context.Users.Update(user);
    public void Delete(User user) => context.Users.Remove(user);
    public async Task<List<User>> GetAllAsync(Expression<Func<User, object>> sortExpression, int page, int size, CancellationToken cancellationToken = default)
    {
        IQueryable<User> usersQuery = context.Users.AsQueryable();

        usersQuery = usersQuery.OrderBy(sortExpression);
        usersQuery = usersQuery.Skip((page - 1) * size).Take(size);

        List<User> users = await usersQuery.ToListAsync(cancellationToken);

        return users;
    }
}
