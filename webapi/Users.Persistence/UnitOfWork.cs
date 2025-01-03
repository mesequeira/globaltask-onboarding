using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Abstractions;
using Users.Domain.Abstractions.Interfaces;

namespace Users.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateAuditableProps();
            return _context.SaveChangesAsync(cancellationToken);
        }

        private void UpdateAuditableProps()
        {
            var entities = _context
                            .ChangeTracker
                            .Entries<BaseEntity>();

            foreach(EntityEntry<BaseEntity> entityEntry in entities)
            {
                if (entityEntry.State == EntityState.Added)
                {
                    entityEntry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entityEntry.State == EntityState.Modified)
                {
                    entityEntry.Entity.ModifiedAt = DateTime.UtcNow;
                }
            }
        }
    }
}
