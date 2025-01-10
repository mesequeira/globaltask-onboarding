using Application.Behaviors;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Users.Commands.CreateUser;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

public class CreateUserCommandHandlerTests : IDisposable
{
    private readonly IApplicationDbContext _context;
    private readonly CreateUserCommandHandler _handler;
    private readonly IValidator<CreateUserCommand> _validator;

    public CreateUserCommandHandlerTests()
    {
        _context = InMemoryDbContextFactory.Create();
        _validator = new CreateUserCommandValidator(); 
        _handler = new CreateUserCommandHandler(_context);
        var dbContext = (DbContext)_context;
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
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
        Assert.Equal("Usuario creado correctamente.", result.Message);

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == result.Value);
        Assert.NotNull(user);
        Assert.Equal(command.Name, user.Name);
    }

    [Fact]
    public async Task Handle_Should_ThrowValidationException_When_ValidationFails()
    {
        var command = new CreateUserCommand
        {
            Name = "",
            Email = "invalid-email",
            PhoneNumber = "123",
            Birthday = DateTime.Now
        };

        var validationBehavior = new ValidationBehavior<CreateUserCommand, Result<int>>(new[] { _validator });

        await Assert.ThrowsAsync<ValidationException>(() => validationBehavior.Handle(
            command,
            () => _handler.Handle(command, default),
            default
        ));
    }

    public void Dispose()
    {
        _context?.Dispose();
    }
}
