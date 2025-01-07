namespace Users.Domain.Abstractions;

public abstract class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}
