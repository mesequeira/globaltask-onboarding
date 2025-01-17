namespace Users.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}