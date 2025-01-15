using FluentValidation;
using Users.Web.Application.Abstract;
using Users.Web.Domain.Users.Models;

namespace Users.Web.Application.Users.Validators;

public sealed class CreateUserModelValidator : BaseValidator<CreateUserModel>
{
    public CreateUserModelValidator()
    {
        RuleFor(x => x.UserName)
        .NotEmpty()
        .WithMessage("UserName is required.")
        .MaximumLength(50)
        .WithMessage("UserName cannot exceed 50 characters.");

        RuleFor(x => x.Password)
          .NotEmpty()
          .WithMessage("Password is required.")
          .MinimumLength(8)
          .WithMessage("Password must be at least 8 characters.");

        RuleFor(u => u.Birthday)
            .LessThanOrEqualTo(DateTime.UtcNow.AddYears(-18))
            .WithMessage("User must be at least 18 years old.");

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid email format.");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .Matches(@"^\+?[0-9\s\-\(\)]{7,15}$")
            .WithMessage("Phone number must be a valid format.")
            .MaximumLength(15)
            .WithMessage("Phone number cannot exceed 15 characters.")
            .MinimumLength(7)
            .WithMessage("Phone number must be at least 7 characters.");
    }
}
