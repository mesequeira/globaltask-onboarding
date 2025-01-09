using FluentValidation;

namespace Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El campo 'Name' es obligatorio.")
                .MaximumLength(100).WithMessage("El campo 'Name' no debe exceder los 100 caracteres.")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage("El campo 'Name' solo puede contener letras y espacios.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El campo 'Email' es obligatorio.")
                .EmailAddress().WithMessage("El campo 'Email' debe tener un formato válido.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("El campo 'PhoneNumber' es obligatorio.")
                .Matches(@"^[0-9]{7,15}$").WithMessage("El campo 'PhoneNumber' debe contener entre 7 y 15 dígitos.");

            RuleFor(x => x.Birthday)
                .Must(BeAtLeast18)
                .WithMessage("El usuario debe ser mayor de 18 años.");
        }

        private bool BeAtLeast18(DateTime birthDate)
        {
            var today = DateTime.Today;
            var age = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-age))
                age--;
            return age >= 18;
        }



    }
}
