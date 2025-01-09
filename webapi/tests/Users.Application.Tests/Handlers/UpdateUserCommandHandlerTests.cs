using Application.Users.Commands.UpdateUser;
using Application.Common.Interfaces;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using Users.Domain.Users.Models;
using System;

namespace Users.Application.Tests.Handlers
{
    public class UpdateUserCommandHandlerTests : IDisposable
    {
        private readonly IApplicationDbContext _context;
        private readonly UpdateUserCommandHandler _handler;

        public UpdateUserCommandHandlerTests()
        {
            _context = InMemoryDbContextFactory.Create();
            _handler = new UpdateUserCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_Should_UpdateUser_Successfully()
        {
            var user = new User { Name = "Original Name", Email = "original@example.com" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var command = new UpdateUserCommand
            {
                Id = user.Id,
                Name = "Updated Name",
                Email = "updated@example.com"
            };

            var result = await _handler.Handle(command, default);

            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.StatusCode);

            var updatedUser = await _context.Users.FindAsync(user.Id);
            Assert.NotNull(updatedUser);
            Assert.Equal(command.Name, updatedUser.Name);
        }

        [Fact]
        public async Task Handle_Should_Throw_KeyNotFoundException_When_UserDoesNotExist()
        {
            var command = new UpdateUserCommand { Id = 99 };

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, default));
        }

        public async void Dispose()
        {
            _context.Users.RemoveRange(_context.Users);
            await _context.SaveChangesAsync();
        }
    }
}
