namespace Users.Domain.Users.Models;

public class BaseModel
{
    public int Id { get; set; }
    public Guid? Guid { get; set; }
    public DateTime ModifiedAt { get; set; }
    public DateTime CretaedAt { get; set; }
}