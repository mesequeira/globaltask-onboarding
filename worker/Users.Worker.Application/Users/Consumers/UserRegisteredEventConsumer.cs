using MassTransit;
using Users.Application.Users.Events;
using Users.Worker.Application.Emails;
using Users.Worker.Application.Users.Events;
using Users.Worker.Domain.Abstractions;
using Users.Worker.Domain.Users.Notifications;

namespace Users.Worker.Application.Users.Consumers;

public sealed class UserRegisteredEventConsumer(IEmailService emailService) : IConsumer<UserRegisteredEvent>
{
    public async Task Consume(ConsumeContext<UserRegisteredEvent> context)
    {
        Console.WriteLine("UserRegisteredEventConsumer");
        EmailMessage message = UserEmailMessages.UserRegistered(context.Message.UserName);

        await emailService.SendEmailAsync(
                context.Message.Email,
                message.Subject,
                message.Body
            );
    }
}
