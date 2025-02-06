using System;

namespace Users.Web.Domain.DTO
{
    public class UserDTO
    {
        public UserDTO()
        {
        }


        public UserDTO(int id, Guid guid, DateTime modifiedAt, DateTime createdAt, DateTime birthDate)
        {
            Id = id;
            Guid = guid;
            ModifiedAt = modifiedAt;
            CreatedAt = createdAt;
            BirthDate = birthDate;
        }
        
        public UserDTO(string name, string email, string phoneNumber, DateTime birthDate)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
            BirthDate = birthDate;
            ModifiedAt = DateTime.Now;
            CreatedAt = DateTime.Now;
        }
        
        public int Id { get; set; }

        public Guid? Guid { get; set; }
        public DateTime ModifiedAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
    }
}



// using Users.Web.Domain.Models;
//
// namespace Users.Web.Domain.DTO;
//
// public class UserDTO : BaseModel
// {
//     public string? Name { get; set; }
//     public string? Email { get; set; }
//     public string? PhoneNumber { get; set; }
//     public DateTime? BirthDate { get; set; }
//
//     public UserDTO()
//     {
//     }
//
//     public UserDTO(string name, string email, string phoneNumber, DateTime birthDate)
//     {
//         Name = name;
//         Email = email;
//         PhoneNumber = phoneNumber;
//         BirthDate = birthDate;
//     }
// }