using System.ComponentModel.DataAnnotations;

namespace Application.Users.Queries
{
    /// <summary>
    /// Solicitud para crear un usuario.
    /// </summary>
    public class CreateUserRequest
    {
        /// <summary>
        /// Nombre del usuario.
        /// </summary>
        /// <example>Juan Pérez</example>
        [Required(ErrorMessage = "El campo 'Name' es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El campo 'Name' no debe exceder los 100 caracteres.")]
        public string Name { get; set; } = "Juan Pérez";

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        /// <example>juan.perez@dominio.com</example>
        [Required(ErrorMessage = "El campo 'Email' es obligatorio.")]
        [EmailAddress(ErrorMessage = "El campo 'Email' debe tener un formato válido.")]
        public string Email { get; set; } = "juan.perez@dominio.com";

        /// <summary>
        /// Número de teléfono del usuario.
        /// </summary>
        /// <example>1234567890</example>
        [Required(ErrorMessage = "El campo 'PhoneNumber' es obligatorio.")]
        [RegularExpression(@"^[0-9]{7,15}$", ErrorMessage = "El campo 'PhoneNumber' debe contener entre 7 y 15 dígitos.")]
        public string PhoneNumber { get; set; } = "1234567890";

        /// <summary>
        /// Fecha de nacimiento del usuario.
        /// </summary>
        /// <example>1990-05-10</example>
        [Required(ErrorMessage = "El campo 'Birthday' es obligatorio.")]
        [DataType(DataType.Date, ErrorMessage = "El campo 'Birthday' debe ser una fecha válida.")]
        public DateTime Birthday { get; set; }
    }

    public class UpdateUserRequest
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? Birthday { get; set; }
    }

    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
    }


}
