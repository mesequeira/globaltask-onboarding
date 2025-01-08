using FluentValidation;

namespace Users.Application.Users.Commands.UpdateUser;

public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserValidator()
    {
        RuleFor(command => command.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Name must be at least 2 characters long.");

        RuleFor(command => command.Email)
            .NotEmpty().WithMessage("El email es obligatorio.")
            .Matches(@"^[^\s@]+@[^\s@]+\.[^\s@]+$").WithMessage("El email no tiene un formato válido.")
            .MaximumLength(254).WithMessage("El email no puede exceder los 254 caracteres.");
        
        RuleFor(command => command.PhoneNumber)
            .NotEmpty().WithMessage("El teléfono es obligatorio.")
            .MinimumLength(7).WithMessage("El teléfono no tiene el mínimo de caracteres (7).")
            .MaximumLength(13).WithMessage("El teléfono tiene más del máximo de caracteres (13).")
            .Matches(@"^\d+$").WithMessage("El teléfono debe contener solo números.");

        
        RuleFor(command => command.BirthDate)
            .NotEmpty().WithMessage("La fecha de nacimiento es obligatoria.")
            .Must(BeAtLeast18YearsOld).WithMessage("Debes tener al menos 18 años.");
    }

    private bool BeAtLeast18YearsOld(DateTime birthDate)
    {
        var today = DateTime.Today;
        var age = today.Year - birthDate.Year;

        // Resta un año si aún no ha cumplido años este año.
        if (birthDate.Date > today.AddYears(-age)) 
            age--;

        return age >= 18;
    }
}
