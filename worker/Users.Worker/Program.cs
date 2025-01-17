using MassTransit;
using Microsoft.Extensions.Options;
using Users.Worker;
using Users.Worker.Application.Emails;
using Users.Worker.Application.Users.Consumers;
using Users.Worker.Infrastructure.Emails;
using Users.Worker.Infrastructure.Emails.Services;
using Users.Worker.Infrastructure.MessageBroker;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddHostedService<Worker>();

// Bind configuration from appsettings.json
IConfiguration configuration = builder.Configuration;

// Configure MessageBrokerSettings
builder.Services.Configure<MessageBrokerSettings>(configuration.GetSection("MessageBroker"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();
Console.WriteLine($"==================== Email Settings ====================");
Console.WriteLine($"SenderEmail: {emailSettings.Email}");
Console.WriteLine($"SenderPassword: {emailSettings.Password}");
Console.WriteLine($"Port: {emailSettings.Port}");
Console.WriteLine($"Host: {emailSettings.Host}");
Console.WriteLine($"========================================================");
// Configure EmailSettings
builder.Services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<EmailSettings>>().Value);

// Configure MassTransit
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.AddConsumers(typeof(UserDeletedEventConsumer).Assembly);

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        var settings = context.GetRequiredService<MessageBrokerSettings>();

        configurator.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.Username);
            h.Password(settings.Password);
        });

        configurator.ConfigureEndpoints(context);
    });
});

// Register email services
builder.Services.AddScoped<IEmailService, EmailService>();

// Build and run the application
var host = builder.Build();
host.Run();