using FluentValidation.TestHelper;
using Users.Application.Users.Commands.UpdateUser;

namespace Users.Tests
{
    public class UpdateUserCommandTests
    {
        [Fact]
        public async Task Update_ShouldReturnNoContent_WhenAllFieldsAreValid()
        {
            // Arrange
            var updateCommand = new UpdateUserCommand(
                1, // ID válido
                "Updated Name",
                "updated.email@example.com",
                "987654321",
                DateTime.Today.AddYears(-25) // Usuario mayor de 18 años
            );

            var validator = new UpdateUserValidator();

            // Act
            var result = await validator.TestValidateAsync(updateCommand);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
            result.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber);
            result.ShouldNotHaveValidationErrorFor(x => x.BirthDate);
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenPhoneIsTooShort()
        {
            // Arrange
            var updateCommand = new UpdateUserCommand(
                1,
                "Valid Name",
                "valid.email@example.com",
                "123", // Teléfono demasiado corto
                DateTime.Today.AddYears(-20)
            );

            var validator = new UpdateUserValidator();

            // Act
            var result = await validator.TestValidateAsync(updateCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber)
                .WithErrorMessage("El teléfono no tiene el mínimo de caracteres (7).");
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenPhoneContainsLetters()
        {
            // Arrange
            var updateCommand = new UpdateUserCommand(
                1,
                "Valid Name",
                "valid.email@example.com",
                "123ABC456", // Teléfono contiene letras
                DateTime.Today.AddYears(-20)
            );

            var validator = new UpdateUserValidator();

            // Act
            var result = await validator.TestValidateAsync(updateCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber)
                .WithErrorMessage("El teléfono debe contener solo números.");
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenPhoneIsTooLong()
        {
            // Arrange
            var updateCommand = new UpdateUserCommand(
                1,
                "Valid Name",
                "valid.email@example.com",
                "123456789012345", // Teléfono demasiado largo
                DateTime.Today.AddYears(-20)
            );

            var validator = new UpdateUserValidator();

            // Act
            var result = await validator.TestValidateAsync(updateCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber)
                .WithErrorMessage("El teléfono tiene más del máximo de caracteres (13).");
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenPhoneIsEmpty()
        {
            // Arrange
            var updateCommand = new UpdateUserCommand(
                1,
                "Valid Name",
                "valid.email@example.com",
                "", // Teléfono vacío
                DateTime.Today.AddYears(-20)
            );

            var validator = new UpdateUserValidator();

            // Act
            var result = await validator.TestValidateAsync(updateCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber)
                .WithErrorMessage("El teléfono es obligatorio.");
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenBirthDateIsEmpty()
        {
            // Arrange
            var updateCommand = new UpdateUserCommand(
                1,
                "Valid Name",
                "valid.email@example.com",
                "1234567890",
                default // Fecha de nacimiento vacía
            );

            var validator = new UpdateUserValidator();

            // Act
            var result = await validator.TestValidateAsync(updateCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BirthDate)
                .WithErrorMessage("La fecha de nacimiento es obligatoria.");
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenUserIsUnder18YearsOld()
        {
            // Arrange
            var updateCommand = new UpdateUserCommand(
                1,
                "Valid Name",
                "valid.email@example.com",
                "1234567890",
                DateTime.Today.AddYears(-10) // Usuario menor de 18 años
            );

            var validator = new UpdateUserValidator();

            // Act
            var result = await validator.TestValidateAsync(updateCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BirthDate)
                .WithErrorMessage("Debes tener al menos 18 años.");
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenEmailIsInvalid()
        {
            // Arrange
            var updateCommand = new UpdateUserCommand(
                1,
                "Valid Name",
                "invalid-email", // Email inválido
                "1234567890",
                DateTime.Today.AddYears(-20)
            );

            var validator = new UpdateUserValidator();

            // Act
            var result = await validator.TestValidateAsync(updateCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("El email no tiene un formato válido.");
        }

        [Fact]
        public async Task Update_ShouldReturnBadRequest_WhenEmailIsEmpty()
        {
            // Arrange
            var updateCommand = new UpdateUserCommand(
                1,
                "Valid Name",
                "", // Email vacío
                "1234567890",
                DateTime.Today.AddYears(-20)
            );

            var validator = new UpdateUserValidator();

            // Act
            var result = await validator.TestValidateAsync(updateCommand);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("El email es obligatorio.");
        }
    }
}
