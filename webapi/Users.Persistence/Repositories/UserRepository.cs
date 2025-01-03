using System.Linq.Expressions;
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

    // public async Task<IEnumerable<User>> Get()
    // {
    //     return  _dbContext.Users.ToList();
    // }
    
    

    public async Task<IQueryable<User>> GetAllAsQueryable()
    {
        return _dbContext.Users.AsQueryable();
    } 
    
    // public async Task<IEnumerable<User>> GetPagedAndSortedAsync(int page = 1, int size = 25, string sortBy = "Name")
    // {
    //     // Obtenemos el query base
    //     var query = _dbContext.Users.AsQueryable();
    //
    //     // Si no encontramos la expresión en el diccionario, usamos una por defecto (Id).
    //     if (!SortExpressions.TryGetValue(sortBy.ToLower(), out var sortExpression))
    //     {
    //         sortExpression = u => u.Id; 
    //     }
    //
    //     // Aplicamos la expresión de ordenación
    //     query = query.OrderBy(sortExpression);
    //
    //     // Calculamos el "skip" y aplicamos paginación
    //     var skip = (page - 1) * size;
    //     query = query.Skip(skip).Take(size);
    //
    //     // Retornamos la lista
    //     return await query.ToListAsync();
    // }
    

    public async Task Create(User usuario)
    {
        usuario.ModifiedAt = usuario.CretaedAt = DateTime.Now;
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