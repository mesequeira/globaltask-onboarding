using FluentValidation;
using Users.Domain.Users.Errors;

namespace Users.Application.Users.Commands.Create;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.UserName)
        .NotEmpty()
        .WithMessage("UserName is required.")
        .WithErrorCode(UserErrorCodes.RequiredUserName)
        .MaximumLength(50)
        .WithMessage("UserName cannot exceed 50 characters.")
        .WithErrorCode(UserErrorCodes.UserNameTooLong);

        RuleFor(x => x.Password)
          .NotEmpty()
          .WithMessage("Password is required.")
          .WithErrorCode(UserErrorCodes.RequiredPassword)
          .MinimumLength(8)
          .WithMessage("Password must be at least 8 characters.")
          .WithErrorCode(UserErrorCodes.InvalidPassword);

        RuleFor(u => u.Birthday)
            .LessThanOrEqualTo(DateTime.UtcNow.AddYears(-18))
            .WithMessage("User must be at least 18 years old.")
            .WithErrorCode(UserErrorCodes.InvalidBirthday);

        RuleFor(x => x.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .WithErrorCode(UserErrorCodes.RequiredEmail)
            .EmailAddress()
            .WithMessage("Invalid email format.")
            .WithErrorCode(UserErrorCodes.InvalidEmailFormat);

        RuleFor(x => x.PhoneNumber)
            .NotEmpty()
            .WithMessage("Phone number is required.")
            .WithErrorCode(UserErrorCodes.RequiredPhoneNumber)
            .Matches(@"^\+?[0-9\s\-\(\)]{7,15}$")
            .WithMessage("Phone number must be a valid format.")
            .WithErrorCode(UserErrorCodes.InvalidPhoneNumber)
            .MaximumLength(15)
            .WithMessage("Phone number cannot exceed 15 characters.")
            .WithErrorCode(UserErrorCodes.PhoneNumberTooLong)
            .MinimumLength(7)
            .WithMessage("Phone number must be at least 7 characters.")
            .WithErrorCode(UserErrorCodes.PhoneNumberTooShort);
    }
}
