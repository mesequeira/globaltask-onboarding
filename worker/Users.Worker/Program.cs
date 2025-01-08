using MassTransit;
using MassTransit.Topology;
using Users.Worker;
using Users.Worker.Application.Users.Consumers;
using Users.Worker.Domain.Users.Events;
using Users.Worker.Infrastructure.MessageBroker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

IConfiguration configuration = builder.Configuration;

MessageBrokerSettings messageBrokerSettings = new MessageBrokerSettings
{
    Host = configuration["MESSAGE_BROKER_HOST"]!,
    Username = configuration["MESSAGE_BROKER_USERNAME"]!,
    Password = configuration["MESSAGE_BROKER_PASSWORD"]!,
};


builder.Services.AddSingleton(messageBrokerSettings);

builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumer<UserRegisteredEventConsumer>();

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();

        configurator.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.Username);
            h.Password(settings.Password);
        });

        configurator.ReceiveEndpoint("user-registered-queue", e =>
        {
            e.ConfigureConsumer<UserRegisteredEventConsumer>(context);
        });

        configurator.Message<UserRegisteredEvent>(x =>
        {
            x.SetEntityName("user-registered-event");
        });

    });
});


var host = builder.Build();
host.Run();
