using Application.Users.Commands.CreateUser;
using Application.Common.Interfaces;
using Moq;
using Xunit;

namespace Users.Application.Tests.Handlers
{
    public class CreateUserCommandHandlerTests
    {
        private readonly Mock<IApplicationDbContext> _mockDbContext;
        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommandHandlerTests()
        {
            // Inicializa el Mock del DbContext
            _mockDbContext = new Mock<IApplicationDbContext>();

            // Configura el comportamiento del método Add para simular la asignación de un ID
            _mockDbContext.Setup(x => x.Users.Add(It.IsAny<Users.Domain.Users.Models.User>()))
                .Callback<Users.Domain.Users.Models.User>(u => u.Id = 1);

            // Configura SaveChangesAsync para simular que guarda correctamente y retorna 1
            _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            // Inicializa el handler con el Mock inyectado
            _handler = new CreateUserCommandHandler(_mockDbContext.Object);
        }

        [Fact]
        public async Task Handle_Should_CreateUser_Successfully()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Name = "Test User",
                Email = "test@example.com",
                PhoneNumber = "123456789",
                Birthday = new DateTime(2000, 1, 1)
            };

            // Act
            var result = await _handler.Handle(command, default);

            // Assert
            _mockDbContext.Verify(x => x.Users.Add(It.IsAny<Users.Domain.Users.Models.User>()), Times.Once);
            _mockDbContext.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
            Assert.True(result > 0);
        }

        [Fact]
        public async Task Handle_Should_ThrowException_When_CommandIsNull()
        {
            // Arrange
            CreateUserCommand command = null;

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(command, default));
        }

        [Fact]
        public async Task Handle_Should_ThrowException_When_CommandHasInvalidData()
        {
            // Arrange
            var command = new CreateUserCommand
            {
                Name = "", // Inválido
                Email = "invalid-email", // Inválido
                PhoneNumber = "123", // Inválido
                Birthday = DateTime.Now
            };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, default));
            Assert.Equal("Invalid user data", exception.Message);
        }
    }
}
