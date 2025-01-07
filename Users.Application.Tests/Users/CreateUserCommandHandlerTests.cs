using FluentAssertions;
using NSubstitute;
using Users.Application.Users.Commands;
using Users.Application.Users.Commands.Create;
using Users.Domain.Abstractions;
using Users.Domain.Abstractions.Interfaces;
using Users.Domain.Users.Abstractions;
using Users.Domain.Users.Errors;

namespace Users.Application.Tests.Users;

public class CreateUserCommandHandlerTests
{
    private readonly IUserRepository _userRepositoryMock;
    private readonly IUnitOfWork _unitOfWorkMock;
    private readonly CreateUserCommandValidator _userValidator;
    private readonly CreateUserCommandHandler _createUserCommandHandler;

    private readonly string UserName = "TestUsername";
    private readonly string Password = "TestPassword";
    private readonly string Email = "test@gmail.com";
    private readonly string PhoneNumber = "1127554941";

    public CreateUserCommandHandlerTests()
    {
        _userRepositoryMock = Substitute.For<IUserRepository>();
        _unitOfWorkMock = Substitute.For<IUnitOfWork>();
        _userValidator = new();

        _createUserCommandHandler = new CreateUserCommandHandler(_userRepositoryMock, _unitOfWorkMock, _userValidator);
    }

    [Fact]
    public async Task Handle_ShouldReturnSuccess_WhenUserIsValid()
    {
        var createUserCommand = new CreateUserCommand(UserName, Email, Password, PhoneNumber, DateTime.UtcNow.AddYears(-20));

        Result<Guid> result = await _createUserCommandHandler.Handle(createUserCommand, default);

        result.Errors.Should().BeEmpty();
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenUserNameIsEmpty()
    {
        var createUserCommand = new CreateUserCommand(string.Empty, Email, Password, PhoneNumber, DateTime.UtcNow.AddYears(-20));

        Result<Guid> result = await _createUserCommandHandler.Handle(createUserCommand, default);

        result.Errors.First().Code.Should().Be(UserErrorCodes.RequiredUserName);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenUserNameIsTooLong()
    {
        var longUserName = new string('a', 51);

        var createUserCommand = new CreateUserCommand(longUserName, Email, Password, PhoneNumber, DateTime.UtcNow.AddYears(-20));

        Result<Guid> result = await _createUserCommandHandler.Handle(createUserCommand, default);

        result.Errors.First().Code.Should().Be(UserErrorCodes.UserNameTooLong);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenAgeIsLessThan18()
    {
        var createUserCommand = new CreateUserCommand(UserName, Email, Password, PhoneNumber, DateTime.UtcNow);

        Result<Guid> result = await _createUserCommandHandler.Handle(createUserCommand, default);

        result.Errors.First().Code.Should().Be(UserErrorCodes.InvalidBirthday);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenEmailIsInvalid()
    {
        var createUserCommand = new CreateUserCommand(UserName, "invalidemail", Password, PhoneNumber, DateTime.UtcNow.AddYears(-20));

        Result<Guid> result = await _createUserCommandHandler.Handle(createUserCommand, default);

        result.Errors.First().Code.Should().Be(UserErrorCodes.InvalidEmailFormat);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenEmailIsEmpty()
    {
        var createUserCommand = new CreateUserCommand(UserName, string.Empty, Password, PhoneNumber, DateTime.UtcNow.AddYears(-20));

        Result<Guid> result = await _createUserCommandHandler.Handle(createUserCommand, default);

        result.Errors.First().Code.Should().Be(UserErrorCodes.RequiredEmail);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenPhoneNumberIsInvalid()
    {
        var createUserCommand = new CreateUserCommand(UserName, Email, Password, "1231@gmail.com", DateTime.UtcNow.AddYears(-20));

        Result<Guid> result = await _createUserCommandHandler.Handle(createUserCommand, default);

        result.Errors.First().Code.Should().Be(UserErrorCodes.InvalidPhoneNumber);
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenPhoneNumberIsEmpty()
    {
        var createUserCommand = new CreateUserCommand(UserName, Email, Password, string.Empty, DateTime.UtcNow.AddYears(-20));

        Result<Guid> result = await _createUserCommandHandler.Handle(createUserCommand, default);

        result.Errors.First().Code.Should().Be(UserErrorCodes.RequiredPhoneNumber);
    }
}
