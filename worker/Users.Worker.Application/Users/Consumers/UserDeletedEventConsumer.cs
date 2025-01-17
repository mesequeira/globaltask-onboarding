using MassTransit;
using Users.Application.Users.Events;
using Users.Worker.Application.Emails;
using Users.Worker.Application.Users.Events;
using Users.Worker.Domain.Abstractions;
using Users.Worker.Domain.Users.Notifications;

namespace Users.Worker.Application.Users.Consumers;

public sealed class UserDeletedEventConsumer(IEmailService emailService) : IConsumer<UserDeletedEvent>
{
    public async Task Consume(ConsumeContext<UserDeletedEvent> context)
    {
        Console.WriteLine("UserDeletedEventConsumer");
        EmailMessage message = UserEmailMessages.UserDeleted(context.Message.UserName, context.Message.Reason);

        await emailService.SendEmailAsync(
                context.Message.Email,
                message.Subject,
                message.Body
            );
    }
}
