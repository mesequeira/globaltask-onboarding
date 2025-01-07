using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Users.Application.Users.Commands.Create;
using Users.Domain.Abstractions.Interfaces;
using Users.Domain.Users.Abstractions;
using Users.Persistence;
using Users.Persistence.Interceptors;
using Users.Persistence.Users.Repositories;
using Users.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

var applicationAssembly = typeof(CreateUserCommand).Assembly;

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(applicationAssembly);
});

builder.Services.AddValidatorsFromAssembly(applicationAssembly);

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