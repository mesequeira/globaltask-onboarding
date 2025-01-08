using MassTransit;
using Users.Worker.Domain.Users.Events;

namespace Users.Worker.Application.Users.Consumers;

public sealed class UserRegisteredEventConsumer : IConsumer<UserRegisteredEvent>
{
    public Task Consume(ConsumeContext<UserRegisteredEvent> context)
    {
        Console.WriteLine(context.Message);

        return Task.CompletedTask;
    }
}
