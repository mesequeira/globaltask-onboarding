using Users.Domain.Users.Models;

namespace Users.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetById(Guid id);
    IEnumerable<User> Get();
    void Crear(User usuario);
    void Actualizar(User usuario); // usando su ID
    void Eliminar(Guid id);
}