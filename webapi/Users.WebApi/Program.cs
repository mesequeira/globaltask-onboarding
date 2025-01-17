using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using Users.Application.Abstractions;
using Users.Application.Users.Commands.CreateUser;
using Users.Application.Users.Events;
using Users.Domain.Interfaces;
using Users.Infrastructure.MessageBroker;
using Users.Infrastructure.Persistence;
using Users.Persistence;
using Users.WebApi.Controllers.User.Examples;
using Users.Worker.Infrastructure.MessageBroker;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// Configuración de MessageBrokerSettings desde appsettings.json
builder.Services.Configure<MessageBrokerSettings>(configuration.GetSection("MessageBrokerSettings"));
builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<MessageBrokerSettings>>().Value);

// Configuración de MassTransit
builder.Services.AddMassTransit(busConfigurator =>
{
    busConfigurator.SetKebabCaseEndpointNameFormatter();

    busConfigurator.UsingRabbitMq((context, configurator) =>
    {
        var settings = context.GetRequiredService<MessageBrokerSettings>();

        Console.WriteLine("RabbitMQ Settings:");
        Console.WriteLine($"Host: {settings.Host}");
        Console.WriteLine($"Username: {settings.Username}");

        configurator.Host(new Uri(settings.Host), h =>
        {
            h.Username(settings.Username);
            h.Password(settings.Password);
        });

        // configurator.Message<UserRegisteredEvent>(config =>
        // {
        //     config.SetEntityName("Users.Worker.Domain.Events:UserRegisteredEvent"); // Nombre del Exchange
        // });
    });
});

// Configuración de DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
});

// Registrar FluentValidation
builder.Services
    .AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>();
    });

// Registrar MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));

// Registrar el Repositorio y UnitOfWork
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IEventBus, EventBus>();

// Agregar servicios de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<CreateUserCommandExample>();

var app = builder.Build();

// Configuración del middleware de Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty; // Hace que Swagger UI esté disponible en la raíz
    });
}

// Middleware para manejar solicitudes
app.MapControllers();

app.Run();
