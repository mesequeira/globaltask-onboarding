using MassTransit;
using NSubstitute;
using Users.Worker.Application.Emails;
using Users.Worker.Application.Users.Consumers;
using Users.Worker.Application.Users.Events;
using Users.Worker.Domain.Abstractions;
using Users.Worker.Domain.Users.Notifications;

namespace Users.Worker.Tests.Users;

public class UserRegisteredEventConsumerTests
{
    private readonly IEmailService _emailServiceMock;

    private readonly string UserName = "TestUserName";
    private readonly string Email = "test@gmail.com";
    public UserRegisteredEventConsumerTests()
    {
        _emailServiceMock = Substitute.For<IEmailService>();
    }

    [Fact]
    public async Task Consume_ShouldSendEmail_WithCorrectParameters()
    {
        UserRegisteredEventConsumer consumer = new(_emailServiceMock);

        UserRegisteredEvent userRegisteredEvent = new(
                Guid.NewGuid(),
                UserName,
                Email
            );

        var contextMock = Substitute.For<ConsumeContext<UserRegisteredEvent>>();
        contextMock.Message.Returns(userRegisteredEvent);

        await consumer.Consume(contextMock);

        EmailMessage message = UserEmailMessages.UserRegistered(UserName);

        await _emailServiceMock.Received(1)
                                .SendEmailAsync(Email, message.Subject, message.Body);
    }
}
