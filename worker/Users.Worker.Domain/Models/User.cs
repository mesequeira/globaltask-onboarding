using Users.Domain.Users.Models;

namespace Users.Domain.Models;

public class User : BaseModel
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public DateTime BirthDate { get; set; }
    
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