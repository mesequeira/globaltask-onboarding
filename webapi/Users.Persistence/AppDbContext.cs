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

        // Configuración adicional de entidades
        modelBuilder.Entity<User>().HasKey(p => p.Id);
    }
    
    public AppDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<AppDbContext>();
        // builder.UseSqlServer("YourConnectionString");
        return new AppDbContext(builder.Options);
    }
    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     Env.Load(@"../.env"); 
    //
    //     // Construye la cadena de conexión
    //     var server = Environment.GetEnvironmentVariable("DB_HOST_MSSQL");
    //     var port = Environment.GetEnvironmentVariable("DB_PORT");
    //     var database = Environment.GetEnvironmentVariable("DB_DATABASE");
    //     var user = Environment.GetEnvironmentVariable("DB_USER");
    //     var password = Environment.GetEnvironmentVariable("DB_PASSWORD");
    //
    //     Console.WriteLine(server);
    //     
    //     // Ejemplo para SQL Server:
    //     var connectionString = 
    //         $"Server=localhost,{port};" +
    //         $"Database={database};" +
    //         $"User Id={user};" +
    //         $"Password={password};" +
    //         "Encrypt=True;" +
    //         "TrustServerCertificate=True;";
    //     
    //     Console.WriteLine(connectionString);
    //     
    //     optionsBuilder.UseSqlServer(connectionString);
    //
    // }
}
