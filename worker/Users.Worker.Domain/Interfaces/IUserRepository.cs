using Users.Domain.Models;
using Users.Domain.Users.Models;

namespace Users.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetById(int id);
    // Task<IEnumerable<User>> Get();
    Task<IQueryable<User>> GetAllAsQueryable();
    // Task<IEnumerable<User>> GetPagedAndSortedAsync(int page = 1, int size = 25, string sortBy = "Name");
    Task Create(User usuario);
    Task Update(User usuario); // usando su ID ? 
    Task<bool> Delete(int id);
}