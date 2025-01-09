using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.UpdateUser;
using Application.Behaviors;

namespace ServiceCollectionExtensions.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Configuración de FluentValidation
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters(); // Para validación en cliente (si es necesario)

            // Registro de validadores en el ensamblado de Application
            services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateUserCommandValidator>();

            // Registro de MediatR
            services.AddMediatR(typeof(CreateUserCommand).Assembly);

            // Registro de AutoMapper
            services.AddAutoMapper(typeof(UserProfile));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlerBehavior<,>));

            return services;
        }
    }
}
