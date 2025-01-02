using Users.Domain.Models;
using Users.Domain.Users.Models;

namespace Users.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetById(Guid id);
    IEnumerable<User> Get();
    void Create(User usuario);
    void Update(User usuario); // usando su ID
    void Delete(Guid id);
}