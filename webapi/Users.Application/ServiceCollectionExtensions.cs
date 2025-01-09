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
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters(); 

            services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<UpdateUserCommandValidator>();

            services.AddMediatR(typeof(CreateUserCommand).Assembly);

            services.AddAutoMapper(typeof(UserProfile));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionHandlerBehavior<,>));

            return services;
        }
    }
}
