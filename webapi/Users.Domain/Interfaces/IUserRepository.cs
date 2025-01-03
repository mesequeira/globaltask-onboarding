using Users.Domain.Models;
using Users.Domain.Users.Models;

namespace Users.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetById(int id);
    Task<IEnumerable<User>> Get();
    Task Create(User usuario);
    Task Update(User usuario); // usando su ID ? 
    Task<bool> Delete(int id);
}