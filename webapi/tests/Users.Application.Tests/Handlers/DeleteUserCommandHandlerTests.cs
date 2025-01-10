using Application.Common.Interfaces;
using Application.Users.Commands.DeleteUser;
using Microsoft.EntityFrameworkCore;
using Users.Domain.Users.Models;

public class DeleteUserCommandHandlerTests : IDisposable
{
    private readonly IApplicationDbContext _context;
    private readonly DeleteUserCommandHandler _handler;

    public DeleteUserCommandHandlerTests()
    {
        _context = InMemoryDbContextFactory.Create();
        _handler = new DeleteUserCommandHandler(_context);

        // Limpia todos los datos previos
        _context.Users.RemoveRange(_context.Users);
        _context.SaveChangesAsync().Wait();
    }

    [Fact]
    public async Task Handle_Should_DeleteUser_Successfully()
    {
        // Arrange
        var user = new User { Name = "User to Delete" };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var command = new DeleteUserCommand { Id = user.Id };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(200, result.StatusCode);
        Assert.Equal("Usuario eliminado correctamente.", result.Message);

        var deletedUser = await _context.Users.FindAsync(user.Id);
        Assert.Null(deletedUser);
    }
    [Fact]
    public async Task Handle_Should_ReturnFailure_When_UserDoesNotExist()
    {
        // Arrange
        var command = new DeleteUserCommand { Id = 99 }; // ID inexistente

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(404, result.StatusCode);
        Assert.Equal("Usuario con ID 99 no encontrado.", result.Error.Description);
    }

    // Implementación correcta de Dispose
    public void Dispose()
    {
        _context?.Dispose();
    }

}
