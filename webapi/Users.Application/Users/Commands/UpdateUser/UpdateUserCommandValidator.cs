using FluentValidation;

namespace Application.Users.Commands.UpdateUser;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("El ID del usuario debe ser mayor a 0.");

        RuleFor(x => x.Name)
            .MaximumLength(255).WithMessage("El campo 'Name' no debe exceder los 255 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.Name));

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("El campo 'Email' debe tener un formato válido.")
            .Must(email => email.Contains("@")).WithMessage("El campo 'Email' debe contener un '@'.")
            .When(x => !string.IsNullOrEmpty(x.Email));

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20).WithMessage("El campo 'PhoneNumber' no debe exceder los 20 caracteres.")
            .When(x => !string.IsNullOrEmpty(x.PhoneNumber));

        RuleFor(x => x.Birthday)
            .Must(BeAtLeast18).WithMessage("El usuario debe ser mayor de 18 años.")
            .When(x => x.Birthday.HasValue);
    }

    private bool BeAtLeast18(DateTime? birthday)
    {
        if (!birthday.HasValue) return true;

        var today = DateTime.Today;
        var age = today.Year - birthday.Value.Year;
        if (birthday.Value.Date > today.AddYears(-age)) age--;
        return age >= 18;
    }
}
