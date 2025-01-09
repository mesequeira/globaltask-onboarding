using Application.Users.Commands.UpdateUser;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Users.Application.Tests.Handlers
{
    public class UpdateUserCommandHandlerTests
    {
        private readonly Mock<IApplicationDbContext> _mockDbContext;
        private readonly UpdateUserCommandHandler _handler;

        public UpdateUserCommandHandlerTests()
        {
            _mockDbContext = new Mock<IApplicationDbContext>();

            // Datos simulados
            var users = new List<Users.Domain.Users.Models.User>
            {
                new Users.Domain.Users.Models.User
                {
                    Id = 1,
                    Name = "Original Name",
                    Email = "original@example.com",
                    PhoneNumber = "123456789",
                    Birthday = new DateTime(1990, 1, 1)
                }
            }.ToMockDbSet();

            _mockDbContext.Setup(x => x.Users).Returns(users.Object);
            _mockDbContext.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

            _handler = new UpdateUserCommandHandler(_mockDbContext.Object);
        }

        [Fact]
        public async Task Handle_Should_UpdateUser_Successfully()
        {
            var command = new UpdateUserCommand
            {
                Id = 1,
                Name = "Updated Name",
                Email = "updated@example.com",
                PhoneNumber = "987654321",
                Birthday = new DateTime(1990, 1, 1)
            };

            await _handler.Handle(command, default);

            _mockDbContext.Verify(x => x.SaveChangesAsync(default), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_Throw_KeyNotFoundException_When_UserDoesNotExist()
        {
            var command = new UpdateUserCommand { Id = 99 };

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, default));
        }
    }
}
