using MassTransit;
using Microsoft.Extensions.Options;
using Users.Worker;
using Users.Worker.Infrastructure.MessageBroker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

// Pasar a Infrastructure
builder.Services.Configure<MessageBrokerSettings>(
    builder.Configuration.GetSection("MessageBroker"));

builder.Services.AddSingleton(sp => 
    sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

builder.Services.AddMassTransit(busConfigugurator =>
{
    busConfigugurator.SetKebabCaseEndpointNameFormatter();

    busConfigugurator.UsingRabbitMq((context, configurator) =>
    {
        MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();

        configurator.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.Username);
            h.Password(settings.Password);
        });

    });
});

var host = builder.Build();
host.Run();
