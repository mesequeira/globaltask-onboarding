using Users.Web.Domain.Models;

namespace Users.Web.Domain.DTO;

public class UserDTO : BaseModel
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public DateTime? BirthDate { get; set; }

    public UserDTO()
    {
    }

    public UserDTO(string name, string email, string phoneNumber, DateTime birthDate)
    {
        Name = name;
        Email = email;
        PhoneNumber = phoneNumber;
        BirthDate = birthDate;
    }
}