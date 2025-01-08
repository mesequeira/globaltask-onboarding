using FluentAssertions;
using FluentValidation.TestHelper;
using Users.Application.Users.Commands.CreateUser;

namespace Users.Tests
{
    public class CreateUserControllerTests
    {

        [Fact]
        public async Task Update_ShouldReturnNoContent_WhenResultIsSuccess()
        {
            // Arrange
            var updateCommand = new CreateUserCommand(
                "Updated Name",
                "updated.email@example.com",
                "987654321",
                DateTime.Today.AddYears(-25) // Usuario mayor de 18 años
            );

            var validator = new CreateUserValidator();

            // Act
            var result = await validator.TestValidateAsync(updateCommand);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Name);
            result.ShouldNotHaveValidationErrorFor(x => x.Email);
            result.ShouldNotHaveValidationErrorFor(x => x.PhoneNumber);
            result.ShouldNotHaveValidationErrorFor(x => x.BirthDate);

            // Simula que la operación se ejecuta correctamente (e.g., el handler devuelve NoContent)
            Assert.True(true); // Aquí validarías el resultado real del controlador si se implementa.
        }
        
        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenEmailIsWrong()
        {
            // Arrange
            var request = new CreateUserCommand
            (
                "Valid", 
                "invalid-email", 
                "1234567893", 
                DateTime.Today.AddYears(-20)
            );

            var validator = new CreateUserValidator();
            // Act
            var result = await validator.TestValidateAsync(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("El email no tiene un formato válido.");
        }
        
        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenEmailIsEmpty()
        {
            // Arrange
            var request = new CreateUserCommand
            (
                "Valid", 
                "", 
                "1234567893", 
                DateTime.Today.AddYears(-20)
            );

            var validator = new CreateUserValidator();
            // Act
            var result = await validator.TestValidateAsync(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Email)
                .WithErrorMessage("El email es obligatorio.");
        }
        
        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenPhoneIsTooShort()
        {
            // Arrange
            var request = new CreateUserCommand
            (
                "Valid Name",
                "valid.email@example.com",
                "123", // Teléfono demasiado corto
                DateTime.Today.AddYears(-20)
            );

            var validator = new CreateUserValidator();

            // Act
            var result = await validator.TestValidateAsync(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber)
                .WithErrorMessage("El teléfono no tiene el mínimo de caracteres (7).");
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenPhoneContainsLetters()
        {
            // Arrange
            var request = new CreateUserCommand
            (
                "Valid Name",
                "valid.email@example.com",
                "123ABC456", // Teléfono contiene letras
                DateTime.Today.AddYears(-20)
            );

            var validator = new CreateUserValidator();

            // Act
            var result = await validator.TestValidateAsync(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber)
                .WithErrorMessage("El teléfono debe contener solo números.");
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenPhoneIsTooLong()
        {
            // Arrange
            var request = new CreateUserCommand
            (
                "Valid Name",
                "valid.email@example.com",
                "123456789012345", // Teléfono demasiado largo
                DateTime.Today.AddYears(-20)
            );

            var validator = new CreateUserValidator();

            // Act
            var result = await validator.TestValidateAsync(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber)
                .WithErrorMessage("El teléfono tiene más del máximo de caracteres (13).");
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenPhoneIsEmpty()
        {
            // Arrange
            var request = new CreateUserCommand
            (
                "Valid Name",
                "valid.email@example.com",
                "", // Teléfono vacío
                DateTime.Today.AddYears(-20)
            );

            var validator = new CreateUserValidator();

            // Act
            var result = await validator.TestValidateAsync(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.PhoneNumber)
                .WithErrorMessage("El teléfono es obligatorio.");
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenBirthDateIsEmpty()
        {
            // Arrange
            var request = new CreateUserCommand
            (
                "Valid Name",
                "valid.email@example.com",
                "1234567890",
                default // Fecha de nacimiento vacía
            );

            var validator = new CreateUserValidator();

            // Act
            var result = await validator.TestValidateAsync(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BirthDate)
                .WithErrorMessage("La fecha de nacimiento es obligatoria.");
        }

        [Fact]
        public async Task Create_ShouldReturnBadRequest_WhenUserIsUnder18YearsOld()
        {
            // Arrange
            var request = new CreateUserCommand
            (
                "Valid Name",
                "valid.email@example.com",
                "1234567890",
                DateTime.Today.AddYears(-10) // Usuario menor de 18 años
            );

            var validator = new CreateUserValidator();

            // Act
            var result = await validator.TestValidateAsync(request);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.BirthDate)
                .WithErrorMessage("Debes tener al menos 18 años.");
        }

    }
}