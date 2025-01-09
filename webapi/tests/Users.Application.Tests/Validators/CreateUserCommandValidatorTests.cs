using Application.Users.Commands.CreateUser;
using Xunit;
using FluentValidation.TestHelper;

namespace Users.Application.Tests.Validators
{
    public class CreateUserCommandValidatorTests
    {
        private readonly CreateUserCommandValidator _validator;

        public CreateUserCommandValidatorTests()
        {
            _validator = new CreateUserCommandValidator();
        }

        [Fact]
        public void Should_HaveValidationError_When_NameIsEmpty()
        {
            var command = new CreateUserCommand { Name = "" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void Should_HaveValidationError_When_EmailIsInvalid()
        {
            var command = new CreateUserCommand { Email = "invalid-email" };
            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Email);
        }

        [Fact]
        public void Should_PassValidation_When_ValidData()
        {
            var command = new CreateUserCommand
            {
                Name = "Test User",
                Email = "test@example.com",
                PhoneNumber = "123456789",
                Birthday = new DateTime(2000, 1, 1)
            };
            var result = _validator.TestValidate(command);
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
