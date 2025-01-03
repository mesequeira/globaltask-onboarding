using Users.Domain.Users.Models;

namespace Users.Domain.Models;

public class User : BaseModel
{
    public string Name { get; protected set; }
    public string Email { get; protected set; }
    public string PhoneNumber { get; protected set; }
    public DateTime BirthDate { get; protected set; }
    
    protected User(int id) => Id = id;
    
    protected User(){}

    public User(int id, string name, string email, string phoneNumber, DateTime birthDate)
    {
        Id = id;
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
    }
    
    public User(string name, string email, string phoneNumber, DateTime birthDate)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
    }
    
    
    
    
}