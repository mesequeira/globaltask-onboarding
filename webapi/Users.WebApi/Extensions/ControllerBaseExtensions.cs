using Microsoft.AspNetCore.Mvc;
using Users.Domain.Abstractions;

namespace Users.WebApi.Extensions
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult GetActionResult(this ControllerBase controller, Result result) 
        { 
            switch (result.StatusCode)
            {
                case 200:
                    return controller.Ok(result);
                case 201:
                    return controller.StatusCode(201, result);
                case 204:
                    return controller.NoContent();
                case 400:
                    return controller.BadRequest(result);
                case 404:
                    return controller.NotFound(result);
                case 500:
                    return controller.StatusCode(500, result);
                default:
                    return controller.Ok(result);
            }
        }
    }
}
