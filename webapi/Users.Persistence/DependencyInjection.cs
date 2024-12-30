using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Users.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        // Configurar el DbContext con la conexión de base de datos
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        // Registrar otros servicios específicos de la capa de Persistence, si los tienes
        // Por ejemplo, repositorios implementados
        // services.AddScoped<IMiRepositorio, MiRepositorio>();

        return services;
    }
}