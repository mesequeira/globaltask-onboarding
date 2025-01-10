using Application.Users.Commands.UpdateUser;
using Application.Common.Interfaces;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using Users.Domain.Users.Models;
using System;
using Microsoft.EntityFrameworkCore;

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
            var dbContext = (DbContext)_context;

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            // Limpiar los datos existentes
            _context.Users.RemoveRange(_context.Users);
            _context.SaveChangesAsync().Wait();

        }
        [Fact]
        public async Task Handle_Should_UpdateUser_Successfully()
        {
            var user = new User { Name = "Original Name", Email = "original@example.com" };
            _context.Users.Add(user);
            await _context.SaveChangesAsync(); // El ID se generará automáticamente

            var command = new UpdateUserCommand
            {
                Id = user.Id, // Usa el ID generado automáticamente
                Name = "Updated Name",
                Email = "updated@example.com"
            };

            var result = await _handler.Handle(command, default);

            Assert.True(result.IsSuccess);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("Usuario actualizado correctamente.", result.Message);

            var updatedUser = await _context.Users.FindAsync(user.Id);
            Assert.NotNull(updatedUser);
            Assert.Equal(command.Name, updatedUser.Name);
            Assert.Equal(command.Email, updatedUser.Email);
        }


        [Fact]
        public async Task Handle_Should_ReturnFailure_When_UserDoesNotExist()
        {
            var command = new UpdateUserCommand { Id = 99, Name = "Nonexistent User", Email = "nonexistent@example.com" };

            var result = await _handler.Handle(command, default);

            Assert.False(result.IsSuccess);
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Usuario con ID 99 no encontrado.", result.Error.Description);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }

}
