using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace Users.WebApi.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                // Manejo de errores de validación
                var errorResponse = new
                {
                    type = "https://brx-onboarding.com/errors",
                    title = "Validation errors occurred.",
                    status = StatusCodes.Status400BadRequest,
                    detail = ex.Message,
                    traceId = context.TraceIdentifier
                };

                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
            catch (Exception ex)
            {
                // Manejo genérico de excepciones
                var errorResponse = new
                {
                    type = "https://brx-onboarding.com/errors",
                    title = "An internal server error occurred.",
                    status = StatusCodes.Status500InternalServerError,
                    detail = ex.Message,
                    traceId = context.TraceIdentifier
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
            }
        }
    }
}
