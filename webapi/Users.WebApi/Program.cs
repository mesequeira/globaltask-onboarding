using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Filters;
using Users.Application;
using Users.Application.Users.Commands.CreateUser;
using Users.Domain.Interfaces;
using Users.Infrastructure.Persistence;
using Users.Persistence;
using Users.WebApi.Controllers.User.Examples;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers()
    .AddFluentValidation(config =>
    {
        config.RegisterValidatorsFromAssemblyContaining<CreateUserValidator>();

        // config.ImplicitlyValidateChildProperties = true;
        // config.AutomaticValidationEnabled = false;
    });

Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    // o UseInMemoryDatabase("UsersDb") para pruebas
});

// // Registrar MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));

// Registrar el Repositorio y UnitOfWork
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();



// Agregar servicios de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.ExampleFilters(); // Register example filters
});

builder.Services.AddSwaggerExamplesFromAssemblyOf<CreateUserCommandExample>(); // Register the example provider


var app = builder.Build();

// Configurar el middleware de Swagger
if (app.Environment.IsDevelopment())
{
    // using (var serviceScope = app.Services.CreateScope())
    // {
    //     var dbContext = serviceScope.ServiceProvider.GetRequiredService<DbContext>();
    //     // Console.WriteLine(builder.Configuration.GetConnectionString("DefaultConnection"));
    //     dbContext.Database.Migrate();
    // }
        
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty; // Esto hace que Swagger UI esté disponible en la raíz
    });
}

// Configurar el middleware para manejar las solicitudes
// app.UseHttpsRedirection();
// app.UseAuthorization();
app.MapControllers();

app.Run();
