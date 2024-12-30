namespace Users.Domain.Users.Models;

public class BaseModel
{
    public Guid Id { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime CretaedAt { get; set; }
}