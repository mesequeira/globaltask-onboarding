using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Users.Worker;
using Users.Worker.Application.Emails;
using Users.Worker.Application.Users.Consumers;
using Users.Worker.Infrastructure.Emails;
using Users.Worker.Infrastructure.Emails.Services;
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

    busConfigurator.AddConsumers(typeof(UserDeletedEventConsumer).Assembly);
    //busConfigurator.AddConsumer<UserRegisteredEventConsumer>();

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();

        configurator.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.Username);
            h.Password(settings.Password);
        });

        configurator.ConfigureEndpoints(context);

        //configurator.ReceiveEndpoint("user-registered-queue", e =>
        //{
        //    e.ConfigureConsumer<UserRegisteredEventConsumer>(context);
        //});


    });
});

EmailSettings emailSettings = new(
    configuration["EMAIL_SENDER_EMAIL"]!, 
    configuration["EMAIL_SENDER_PASSWORD"]!, 
    configuration.GetValue<int>("EMAIL_PORT"), 
    configuration["EMAIL_HOST"]!
);

builder.Services.AddSingleton(emailSettings);

builder.Services.AddScoped<IEmailService, EmailService>();

var host = builder.Build();
host.Run();
