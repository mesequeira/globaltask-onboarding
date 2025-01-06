using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Domain.Abstractions;

namespace Users.Domain.Users.Models
{
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
        {
            if(string.IsNullOrEmpty(email)) 
                throw new ArgumentNullException("email");

            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException("password");

            if (string.IsNullOrEmpty(phoneNumber))
                throw new ArgumentNullException("phoneNumber");

            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException("userName");

            return new User
            {
                Email = email,
                Password = password,
                PhoneNumber = phoneNumber,
                UserName = userName,
                Birthday = birthday
            };
        }
    }
}
