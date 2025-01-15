using FluentValidation;
using Users.Web.Application.Abstract;
using Users.Web.Domain.Users.Models;

namespace Users.Web.Application.Users.Validators;

public sealed class EditUserModelValidator : BaseValidator<UpdateUserModel>
{
    public EditUserModelValidator()
    {
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

        RuleFor(x => x.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .MinimumLength(8)
            .WithMessage("Password must be at least 8 characters.");
    }
}
