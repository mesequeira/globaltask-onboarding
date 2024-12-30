using System.Runtime.CompilerServices;

namespace Users.Domain.Users.Models;

public class User : BaseModel
{
    public string Name { get; protected set; }
    public string Email { get; protected set; }
    public string PhoneNumber { get; protected set; }
    public DateTime BirthDate { get; protected set; }
    
    protected User(Guid id) => Id = id;
    
    protected User(){}

    public User(Guid id, string name, string email, string phoneNumber)
    {
        this.Id = id;
        this.Name = name;
        this.Email = email;
        this.PhoneNumber = phoneNumber;
        this.BirthDate = DateTime.Now;
    }
    
    
    
    
}