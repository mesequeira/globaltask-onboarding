using System;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Users.Worker;
using DotNetEnv;

var builder = Host.CreateApplicationBuilder(args);
// Cargar variables del archivo .env
Env.Load();

// Acceder a las variables de entorno en el código
var rabbitHost = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "rabbitmq";
var rabbitUser = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_USER");
var rabbitPass = Environment.GetEnvironmentVariable("RABBITMQ_DEFAULT_PASS");
var rabbitConfig = builder.Configuration.GetSection("RabbitMQ");

builder.Services.AddHostedService<Worker>();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host(rabbitHost, "/", h =>
        {
            h.Username(rabbitUser);
            h.Password(rabbitPass);
        });
    });
});

var host = builder.Build();
host.Run();
