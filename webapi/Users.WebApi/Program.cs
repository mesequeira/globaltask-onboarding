using Microsoft.EntityFrameworkCore;
using Users.Application.Users.Commands.CreateUser;
using Users.Domain.Interfaces;
using Users.Infrastructure.Persistence;
using Users.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    // o UseInMemoryDatabase("UsersDb") para pruebas
});

// // Registrar MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));

// Registrar el Repositorio y UnitOfWork
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();


// Agregar servicios de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

// Configurar el middleware de Swagger
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
        c.RoutePrefix = string.Empty; // Esto hace que Swagger UI esté disponible en la raíz
    });
// }

// Configurar el middleware para manejar las solicitudes
// app.UseHttpsRedirection();
// app.UseAuthorization();
app.MapControllers();

app.Run();




// 1. Agregar DbContext


// 2. Registrar MediatR
// builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));

// 3. Registrar el Repositorio y UnitOfWork
// builder.Services.AddScoped<IUserRepository, UserRepository>();
// builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// 4. Agregar controladores
// builder.Services.AddControllers();