using MassTransit;
using Users.Worker.Application.Emails;
using Users.Worker.Application.Users.Events;
using Users.Worker.Domain.Abstractions;
using Users.Worker.Domain.Users.Notifications;

namespace Users.Worker.Application.Users.Consumers;

public sealed class UserUpdatedEventConsumer(IEmailService emailService) : IConsumer<UserUpdatedEvent>
{
    public async Task Consume(ConsumeContext<UserUpdatedEvent> context)
    {
        Console.WriteLine($"User {context.Message.Email} has been updated");
        EmailMessage message = UserEmailMessages.UserUpdated(context.Message.ModifiedFields);

        await emailService.SendEmailAsync(
                context.Message.Email,
                message.Subject,
                message.Body
            );
    }
}
