using Azure.Identity;
using Microsoft.EntityFrameworkCore;
using Users.Domain.Interfaces;
using Users.Domain.Models;

namespace Users.Persistence;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User?> GetById(int id)
    {
        return _dbContext.Set<User>().FirstOrDefault(x => x.Id == id);
    }

    public async Task<IEnumerable<User>> Get()
    {
        return  _dbContext.Users.ToList();
    }

    public async Task Create(User usuario)
    {
        _dbContext.Set<User>().Add(usuario);
    }

    public async Task Update(User usuario)
    {
        _dbContext.Set<User>().Update(usuario);
    }

    public async Task<bool> Delete(int id)
    {
        var success = false;
        var user = _dbContext.Set<User>().FirstOrDefault(x => x.Id == id);
        if (user == null) return success;
        
        _dbContext.Set<User>().Remove(user);
        success = true;

        return success;
    }
}