using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Behaviors
{
    public class ExceptionHandlerBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (FluentValidation.ValidationException ex)
            {
               
                var distinctErrors = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .Select(g => new { Property = g.Key, Messages = g.Select(e => e.ErrorMessage).Distinct() });

                var detailMessage = string.Join(", ", distinctErrors.SelectMany(e => e.Messages));
                throw new ApplicationException($"Errores de validación: {detailMessage}");
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error inesperado", ex);
            }
        }
    }

}
