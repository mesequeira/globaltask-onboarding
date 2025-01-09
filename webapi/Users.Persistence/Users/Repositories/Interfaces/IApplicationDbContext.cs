using Microsoft.EntityFrameworkCore;
using Users.Domain.Users.Models;

namespace Persistence.Repositories.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
