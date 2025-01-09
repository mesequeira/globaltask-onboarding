using MassTransit;
using NSubstitute;
using Users.Worker.Application.Emails;
using Users.Worker.Application.Users.Consumers;
using Users.Worker.Application.Users.Events;
using Users.Worker.Domain.Abstractions;
using Users.Worker.Domain.Users.Notifications;

namespace Users.Worker.Tests.Users;

public class UserDeletedEventConsumerTests
{
    private readonly IEmailService _emailServiceMock;

    private readonly string UserName = "TestUserName";
    private readonly string Email = "test@gmail.com";
    private readonly string Reason = "Inactivity";
    public UserDeletedEventConsumerTests()
    {
        _emailServiceMock = Substitute.For<IEmailService>();
    }

    [Fact]
    public async Task Consume_ShouldSendEmail_WithCorrectParameters()
    {
        UserDeletedEventConsumer consumer = new(_emailServiceMock);

        UserDeletedEvent userDeletedEvent = new(
                UserName,
                Email,
                Reason
            );

        var contextMock = Substitute.For<ConsumeContext<UserDeletedEvent>>();
        contextMock.Message.Returns(userDeletedEvent);

        await consumer.Consume(contextMock);

        EmailMessage message = UserEmailMessages.UserDeleted(UserName, Reason);

        await _emailServiceMock.Received(1)
                                .SendEmailAsync(Email, message.Subject, message.Body);
    }
}
