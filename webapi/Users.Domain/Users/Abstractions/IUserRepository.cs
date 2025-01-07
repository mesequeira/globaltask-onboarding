using System.Linq.Expressions;
using Users.Domain.Users.Models;

namespace Users.Domain.Users.Abstractions;

public interface IUserRepository
{
    Task<List<User>> GetAllAsync(
        Expression<Func<User, object>> sortExpression, 
        int page, 
        int size, 
        CancellationToken cancellationToken = default
    );
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    void Insert(User user);
    void Update(User user);
    void Delete(User user);
}
