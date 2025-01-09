using MassTransit;
using NSubstitute;
using Users.Worker.Application.Emails;
using Users.Worker.Application.Users.Consumers;
using Users.Worker.Application.Users.Events;
using Users.Worker.Domain.Abstractions;
using Users.Worker.Domain.Users.Notifications;

namespace Users.Worker.Tests.Users;

public class UserUpdatedEventConsumerTests
{
    private readonly IEmailService _emailServiceMock;
    public UserUpdatedEventConsumerTests()
    {
        _emailServiceMock = Substitute.For<IEmailService>();
    }

    [Fact]
    public async Task Consume_ShouldSendEmail_WithCorrectParameters()
    {
        string newEmail = "newEmail@gmail.com";
        string oldEmail = "oldEmail@gmail.com";

        FieldChange fieldChange = new(oldEmail, newEmail);

        Dictionary<string, FieldChange> fieldsChanged = new()
        {
            { "Email", fieldChange }
        };

        UserUpdatedEventConsumer consumer = new(_emailServiceMock);

        UserUpdatedEvent userUpdatedEvent = new(
                newEmail,
                fieldsChanged
        );

        var contextMock = Substitute.For<ConsumeContext<UserUpdatedEvent>>();
        contextMock.Message.Returns(userUpdatedEvent);

        await consumer.Consume(contextMock);

        EmailMessage message = UserEmailMessages.UserUpdated(fieldsChanged);

        await _emailServiceMock.Received(1)
                                .SendEmailAsync(newEmail, message.Subject, message.Body);
    }
}
