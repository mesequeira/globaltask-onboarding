using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Users.Domain.Users.Models;

namespace Users.Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Name)
                      .HasColumnType("VARCHAR(255)")
                      .IsRequired();

                entity.Property(e => e.Email)
                      .HasColumnType("VARCHAR(255)")
                      .IsRequired();

                entity.Property(e => e.PhoneNumber)
                      .HasColumnType("VARCHAR(20)");

                entity.Property(e => e.Birthday)
                      .HasColumnType("DATE") 
                      .IsRequired();

                entity.Property(e => e.CreatedAt)
                      .HasColumnType("DATETIME")
                      .IsRequired();

                entity.Property(e => e.ModifiedAt)
                      .HasColumnType("DATETIME")
                      .IsRequired();
            });
        }
    }
}
