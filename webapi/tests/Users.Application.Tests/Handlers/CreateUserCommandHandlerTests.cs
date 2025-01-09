using Application.Users.Commands.CreateUser;
using Application.Common.Interfaces;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using Users.Domain.Users.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Users.Application.Tests.Handlers
{
    public class CreateUserCommandHandlerTests : IDisposable
    {
        private readonly IApplicationDbContext _context;
        private readonly CreateUserCommandHandler _handler;

        public CreateUserCommandHandlerTests()
        {
            _context = InMemoryDbContextFactory.Create();
            _handler = new CreateUserCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_Should_CreateUser_Successfully()
        {
            var command = new CreateUserCommand
            {
                Name = "Test User",
                Email = "test@example.com",
                PhoneNumber = "123456789",
                Birthday = new DateTime(2000, 1, 1)
            };

            var result = await _handler.Handle(command, default);

            Assert.True(result.IsSuccess);
            Assert.Equal(201, result.StatusCode);

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == result.Value);
            Assert.NotNull(user);
            Assert.Equal(command.Name, user.Name);
        }

        [Fact]
        public async Task Handle_Should_ReturnValidationError_When_CommandHasInvalidData()
        {
            var command = new CreateUserCommand
            {
                Name = "",
                Email = "invalid-email",
                PhoneNumber = "123",
                Birthday = DateTime.Now
            };

            var result = await _handler.Handle(command, default);

            Assert.False(result.IsSuccess);
            Assert.Equal(400, result.StatusCode);
            Assert.Equal("ValidationError", result.Error.Code);
        }

        public async void Dispose()
        {
            _context.Users.RemoveRange(_context.Users);
            await _context.SaveChangesAsync();
        }
    }
}
