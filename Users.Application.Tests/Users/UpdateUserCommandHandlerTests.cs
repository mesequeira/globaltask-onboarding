using FluentAssertions;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Users.Application.Users.Commands;
using Users.Application.Users.Dtos;
using Users.Application.Users.Handlers;
using Users.Application.Users.Validators;
using Users.Domain.Abstractions;
using Users.Domain.Abstractions.Interfaces;
using Users.Domain.Users.Abstractions;
using Users.Domain.Users.Errors;
using Users.Domain.Users.Models;

namespace Users.Application.Tests.Users
{
    public class UpdateUserCommandHandlerTests
    {
        private readonly IUserRepository _userRepositoryMock;
        private readonly IUnitOfWork _unitOfWorkMock;
        private readonly UpdateUserCommandValidator _userValidator;
        private readonly UpdateUserCommandHandler _updateUserCommandHandler;

        private readonly Guid randomGuid = Guid.NewGuid();
        private readonly string Password = "TestPassword";
        private readonly string Email = "test@gmail.com";
        private readonly string PhoneNumber = "1127554941";

        public UpdateUserCommandHandlerTests()
        {
            _userRepositoryMock = Substitute.For<IUserRepository>();
            _unitOfWorkMock = Substitute.For<IUnitOfWork>();
            _userValidator = new();

            _updateUserCommandHandler = new UpdateUserCommandHandler(_userRepositoryMock, _unitOfWorkMock, _userValidator);
        }

        [Fact]
        public async Task Handle_ShouldReturnNotFound_WhenUserNotFound()
        {
            _userRepositoryMock.GetByIdAsync(randomGuid).Returns(Task.FromResult<User?>(null));

            UpdateUserCommand updateUserCommand = new(randomGuid, Password, PhoneNumber, Email);

            Result result = await _updateUserCommandHandler.Handle(updateUserCommand, default);

            result.Errors.First().Code.Should().Be(UserErrorCodes.NotFound);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenEmailIsInvalid()
        {
            UpdateUserCommand updateUserCommand = new(randomGuid, Password, PhoneNumber, "invalidemail");

            Result result = await _updateUserCommandHandler.Handle(updateUserCommand, default);

            result.Errors.First().Code.Should().Be(UserErrorCodes.InvalidEmailFormat);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenEmailIsEmpty()
        {
            UpdateUserCommand updateUserCommand = new(randomGuid, Password, PhoneNumber, string.Empty);

            Result result = await _updateUserCommandHandler.Handle(updateUserCommand, default);

            result.Errors.First().Code.Should().Be(UserErrorCodes.RequiredEmail);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenPhoneNumberIsInvalid()
        {
            UpdateUserCommand updateUserCommand = new(randomGuid, Password, "1234", Email);

            Result result = await _updateUserCommandHandler.Handle(updateUserCommand, default);

            result.Errors.First().Code.Should().Be(UserErrorCodes.InvalidPhoneNumber);
        }

        [Fact]
        public async Task Handle_ShouldReturnError_WhenPhoneNumberIsEmpty()
        {
            UpdateUserCommand updateUserCommand = new(randomGuid, Password, string.Empty, Email);

            Result result = await _updateUserCommandHandler.Handle(updateUserCommand, default);

            result.Errors.First().Code.Should().Be(UserErrorCodes.RequiredPhoneNumber);
        }
    }
}
