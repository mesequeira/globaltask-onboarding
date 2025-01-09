using Application.Users.Commands.DeleteUser;
using Application.Common.Interfaces;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using Users.Domain.Users.Models;
using System;

namespace Users.Application.Tests.Handlers
{
    public class DeleteUserCommandHandlerTests : IDisposable
    {
        private readonly IApplicationDbContext _context;
        private readonly DeleteUserCommandHandler _handler;

        public DeleteUserCommandHandlerTests()
        {
            _context = InMemoryDbContextFactory.Create();
            _handler = new DeleteUserCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_Should_DeleteUser_Successfully()
        {
            var user = new User { Name = "User to Delete" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var command = new DeleteUserCommand { Id = user.Id };

            var result = await _handler.Handle(command, default);

            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.StatusCode);

            var deletedUser = await _context.Users.FindAsync(user.Id);
            Assert.Null(deletedUser);
        }

        [Fact]
        public async Task Handle_Should_Throw_KeyNotFoundException_When_UserDoesNotExist()
        {
            var command = new DeleteUserCommand { Id = 99 };

            await Assert.ThrowsAsync<KeyNotFoundException>(() => _handler.Handle(command, default));
        }

        public async void Dispose()
        {
            _context.Users.RemoveRange(_context.Users);
            await _context.SaveChangesAsync();
        }
    }
}
