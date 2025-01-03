// Infrastructure/Persistence/UnitOfWork.cs

using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Users.Domain.Interfaces;
using Users.Persistence;

namespace Users.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
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