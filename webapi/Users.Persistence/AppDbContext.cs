using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Users.Domain.Models;
using DotNetEnv;


namespace Users.Persistence;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } 
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        // Configuración adicional de entidades
    }
    
    // public AppDbContext CreateDbContext(string[] args)
    // {
    //     var builder = new DbContextOptionsBuilder<AppDbContext>();
    //     // builder.UseSqlServer("YourConnectionString");
    //     return new AppDbContext(builder.Options);
    // }
    
}
