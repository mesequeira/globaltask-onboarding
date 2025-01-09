using FluentValidation;
using MediatR;
using Application.Common.Models;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Behaviors
{
    public class ExceptionHandlerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result<object>, new() 
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (ValidationException ex)
            {
                var errors = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(e => e.ErrorMessage).Distinct().ToArray()
                    );

                return new TResponse
                {
                    IsSuccess = false,
                    StatusCode = 400,
                    Error = new ErrorDetails
                    {
                        Code = "ValidationError",
                        Description = "Errores de validación en la solicitud.",
                        Type = "Validation",
                    },
                    Message = string.Join(", ", errors.SelectMany(e => e.Value))
                };
            }
            catch (Exception ex)
            {
                return new TResponse
                {
                    IsSuccess = false,
                    StatusCode = 500,
                    Error = new ErrorDetails
                    {
                        Code = "InternalServerError",
                        Description = ex.Message,
                        Type = "Exception",
                    }
                };
            }
        }
    }
}
