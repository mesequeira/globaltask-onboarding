using Application.Users.Commands.DeleteUser;
using Application.Common.Interfaces;
using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using System;

namespace Users.Application.Tests.Handlers
{
    public class DeleteUserCommandHandlerTests
    {
        private readonly Mock<IApplicationDbContext> _mockDbContext;
        private readonly DeleteUserCommandHandler _handler;

        public DeleteUserCommandHandlerTests()
        {
            _mockDbContext = new Mock<IApplicationDbContext>();
            _handler = new DeleteUserCommandHandler(_mockDbContext.Object);
        }

        [Fact]
        public async Task Handle_Should_DeleteUser_Successfully()
        {
            // Arrange
            var command = new DeleteUserCommand { Id = 1 };
            var user = new Users.Domain.Users.Models.User { Id = 1 };

            _mockDbContext.Setup(x => x.Users.FindAsync(new object[] { command.Id }, It.IsAny<CancellationToken>()))
                          .ReturnsAsync(user);

            _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
                          .ReturnsAsync(1);

            // Act
            await _handler.Handle(command, default);

            // Assert
            _mockDbContext.Verify(x => x.Users.Remove(user), Times.Once);
            _mockDbContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Throw_KeyNotFoundException_When_UserDoesNotExist()
        {
            // Arrange
            var command = new DeleteUserCommand { Id = 99 };

            _mockDbContext.Setup(x => x.Users.FindAsync(new object[] { command.Id }, It.IsAny<CancellationToken>()))
                          .ReturnsAsync((Users.Domain.Users.Models.User)null);

            // Act & Assert
            await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, default));
        }
    }
}
