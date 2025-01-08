using FluentValidation;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Users.Application.Abstractions;
using Users.Application.Users.Commands.Create;
using Users.Domain.Abstractions.Interfaces;
using Users.Domain.Users.Abstractions;
using Users.Domain.Users.Events;
using Users.Infrastructure.MessageBroker;
using Users.Persistence;
using Users.Persistence.Interceptors;
using Users.Persistence.Users.Repositories;
using Users.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

var applicationAssembly = typeof(CreateUserCommand).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(applicationAssembly);
});

builder.Services.AddValidatorsFromAssembly(applicationAssembly);

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

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        MessageBrokerSettings settings = context.GetRequiredService<MessageBrokerSettings>();

        configurator.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.Username);
            h.Password(settings.Password);
        });

        configurator.Message<UserRegisteredEvent>(x =>
        {
            x.SetEntityName("user-registered-event");
        });
    });

});

builder.Services.AddTransient<IEventBus, EventBus>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddSingleton<UpdateAuditablePropsInterceptor>();

builder.Services.AddDbContext<ApplicationDbContext>(
        (sp, options) => options
            .UseSqlServer(builder.Configuration.GetConnectionString("ConnectionString"))
            .AddInterceptors(sp.GetRequiredService<UpdateAuditablePropsInterceptor>())
        );

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.MapControllers();

app.Run();