using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Abstractions;

namespace Users.Persistence.Interceptors
{
    public sealed class UpdateAuditablePropsInterceptor : SaveChangesInterceptor
    {
        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            if(eventData.Context is not null)
            {
                UpdateAuditableProps(eventData.Context);
            }

            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateAuditableProps(DbContext context)
        {
            var entities = context
                            .ChangeTracker
                            .Entries<BaseEntity>();

            foreach (EntityEntry<BaseEntity> entityEntry in entities)
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
