using Microsoft.EntityFrameworkCore;
using Users.Domain.Interfaces;
using Users.Domain.Models;

namespace Users.Persistence;

public class UserRepository : IUserRepository
{
    private readonly DbContext _dbContext;

    public UserRepository(DbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetById(Guid id)
    {
        return _dbContext.Set<User>().FirstOrDefault(x => x.Id == id);
    }

    public IEnumerable<User> Get()
    {
        return _dbContext.Set<User>();
    }

    public void Create(User usuario)
    {
        _dbContext.Set<User>().Add(usuario);
    }

    public void Update(User usuario)
    {
        _dbContext.Set<User>().Update(usuario);
    }

    public void Delete(Guid id)
    {
        var user = _dbContext.Set<User>().FirstOrDefault(x => x.Id == id);
        if (user != null) _dbContext.Set<User>().Remove(user);
    }
}