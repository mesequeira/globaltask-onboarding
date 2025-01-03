using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Users.Commands;

namespace Users.Application.Users.Validators
{
    public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator() 
        {
            RuleFor(x => x.UserName)
            .NotEmpty()
            .WithMessage("UserName is required.")
            .WithErrorCode("User.NoUserNameProvided")
            .MaximumLength(50)
            .WithMessage("UserName cannot exceed 50 characters.")
            .WithErrorCode("User.InvalidUserName");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .WithErrorCode("User.NoEmailProvided")
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .WithErrorCode("User.InvalidEmail");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .WithErrorCode("User.NoPasswordProvided")
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters.")
                .WithErrorCode("User.InvalidPassword");

            RuleFor(u => u.Birthday)
                .LessThanOrEqualTo(DateTime.UtcNow.AddYears(-18))
                .WithMessage("User must be at least 18 years old.")
                .WithErrorCode("User.InvalidBirthday");

            RuleFor(x => x.PhoneNumber)
                    .NotEmpty().WithMessage("Phone number is required.").WithErrorCode("User.NoPhoneNumberProvided")
                    .Matches(@"^\+?[0-9\s\-\(\)]{7,15}$")
                    .WithMessage("Phone number must be a valid format.")
                    .WithErrorCode("User.InvalidPhoneNumber")
                    .MaximumLength(15)
                    .WithMessage("Phone number cannot exceed 15 characters.")
                    .WithErrorCode("User.PhoneNumberTooLong")
                    .MinimumLength(7)
                    .WithMessage("Phone number must be at least 7 characters.")
                    .WithErrorCode("User.PhoneNumberTooShort");
        }
    }
}
