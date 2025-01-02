using Users.Domain.Users.Models;

namespace Users.Domain.Models;

public class User : BaseModel
{
    public string Name { get; protected set; }
    public string Email { get; protected set; }
    public string PhoneNumber { get; protected set; }
    public DateTime BirthDate { get; protected set; }
    
    protected User(Guid id) => Id = id;
    
    protected User(){}

    public User(Guid id, string name, string email, string phoneNumber, DateTime birthDate)
    {
        this.Id = id;
        this.Name = name;
        this.Email = email;
        this.PhoneNumber = phoneNumber;
        this.BirthDate = birthDate;
    }
    
    
    
    
}