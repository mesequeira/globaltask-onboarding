using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Users.Commands;
using Users.Domain.Users.Errors;

namespace Users.Application.Users.Validators
{
    public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator() 
        {
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

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required.")
                .WithErrorCode(UserErrorCodes.RequiredPassword)
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters.")
                .WithErrorCode(UserErrorCodes.InvalidPassword);
        }
    }
}
