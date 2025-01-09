using Application.Users.Commands.UpdateUser;
using Xunit;

namespace Users.Application.Tests.Validators
{
    public class UpdateUserCommandValidatorTests
    {
        private readonly UpdateUserCommandValidator _validator;

        public UpdateUserCommandValidatorTests()
        {
            _validator = new UpdateUserCommandValidator();
        }

        [Fact]
        public void Validate_Should_Pass_For_ValidCommand()
        {
            // Arrange
            var command = new UpdateUserCommand
            {
                Id = 1,
                Name = "Valid Name",
                Email = "valid@example.com",
                PhoneNumber = "123456789",
                Birthday = new DateTime(2000, 1, 1)
            };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.True(result.IsValid);
        }

        [Fact]
        public void Validate_Should_Fail_When_IdIsInvalid()
        {
            // Arrange
            var command = new UpdateUserCommand { Id = 0 };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
        }

        [Fact]
        public void Validate_Should_Fail_When_EmailIsInvalid()
        {
            // Arrange
            var command = new UpdateUserCommand { Id = 1, Email = "invalid-email" };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.False(result.IsValid);
        }
    }
}
