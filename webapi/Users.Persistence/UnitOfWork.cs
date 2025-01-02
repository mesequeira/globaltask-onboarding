// Infrastructure/Persistence/UnitOfWork.cs

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Users.Domain.Interfaces;

namespace Users.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;

    public UnitOfWork(DbContext context)
    {
        _context = context;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}