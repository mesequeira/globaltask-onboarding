using Users.Domain.Abstractions;

namespace Users.Domain.Users.Models;

public class User : BaseEntity
{
    public string UserName { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime Birthday { get; set; }
    public string PhoneNumber { get; set; } = null!;
    public void Update(string email, string password, string phoneNumber)
    {
        Email = email;
        Password = password;
        PhoneNumber = phoneNumber;
    }

    public static User Create(string email, string password, string phoneNumber, string userName,DateTime birthday)
            => new User
                {
                    Email = email,
                    Password = password,
                    PhoneNumber = phoneNumber,
                    UserName = userName,
                    Birthday = birthday
                };
}
