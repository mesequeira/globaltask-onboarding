using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using Users.Application.DTOs.Users;
using Users.Application.Users.Commands.CreateUser;
using Users.Application.Users.Commands.DeleteUser;
using Users.Application.Users.Commands.UpdateUser;
using Users.Application.Users.Events;
using Users.Application.Users.Queries.GetAllUsers;
using Users.Application.Users.Queries.GetUserById;
using Users.Domain.Abstractions;
using Users.WebApi.Controllers.User.Examples;
using Users.Worker.Application.Users.Events;
using Users.Worker.Domain.Abstractions;


namespace Users.WebApi.Controllers.User;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    
    private readonly IMediator _mediator;
    private readonly IPublishEndpoint _publishEndpoint;
    
    public UsersController(IMediator mediator, IPublishEndpoint publishEndpoint)
    {
        _mediator = mediator;
        _publishEndpoint = publishEndpoint;
    }
    
    // POST: api/users
    [HttpPost]
    [SwaggerOperation(
        Summary = "Crea un nuevo usuario",
        Description = "Crea un usuario y retorna el Id asignado en el campo 'value'."
    )]
    [SwaggerRequestExample(typeof(CreateUserCommand), typeof(CreateUserCommandExample))]
    [SwaggerResponse(201, "El usuario fue creado correctamente.", typeof(Result<int>))]
    [SwaggerResponse(400, "Error de validación en los datos de entrada.")]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var result  = await _mediator.Send(command);
        
        if (!result.IsSuccess)
        {
            return BadRequest(result.CreateResponseObject());
        }
        
        var userRegisteredEvent = new UserRegisteredEvent
        (
            result.Value,
            command.Name,
            command.Email
        );

        await _publishEndpoint.Publish(userRegisteredEvent);
        
        return CreatedAtAction(
            nameof(GetById), 
            new { id = result.Value },
            result.CreateResponseObject()
        );
    }

    // GET: api/users
    [HttpGet]
    [SwaggerOperation(
        Summary = "Obtiene un listado paginado de usuarios",
        Description = "Retorna la lista de usuarios paginada en el campo 'value'."
    )]
    [SwaggerResponse(200, "Lista de usuarios paginada.", typeof(Result<PaginatedUsersDTO>))]
    [SwaggerResponse(400, "Parámetros de paginación inválidos.")]
    public async Task<IActionResult> GetUsers(
        [FromQuery] int page = 1,
        [FromQuery] int size = 25,
        [FromQuery] string sortBy = "name")
    {
        var query = new GetAllUsersQuery(page, size, sortBy);
        var result = await _mediator.Send(query);
        if (result.IsSuccess)
           return Ok(result.CreateResponseObject());
        return BadRequest(result.CreateResponseObject());
    }

    // GET: api/users/{id}
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Obtiene la información de un usuario por Id",
        Description = "Retorna la información del usuario en el campo 'value'."
    )]
    [SwaggerResponse(200, "Datos del usuario.")]
    [SwaggerResponse(404, "No se encontró un usuario con ese Id.")]
    public async Task<IActionResult> GetById(int id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await _mediator.Send(query);

        return result.StatusCode switch
        {
            200 => Ok(result.CreateResponseObject()),
            404 => NotFound(result.CreateResponseObject()),
            _ => BadRequest(result.CreateResponseObject())
        };
    }

    // PATCH: api/users/{id}
    [HttpPatch("{id:int}")]
    [SwaggerOperation(
        Summary = "Actualiza los datos de un usuario",
        Description = "Retorna un objeto Result con 'isSuccess=true' y sin valor en caso de éxito (campo 'value' = null)."
    )]
    [SwaggerRequestExample(typeof(UpdateUserCommand), typeof(UpdateUserCommandExample))]
    [SwaggerResponse(200, "Usuario actualizado correctamente.", typeof(Result<int>))]
    [SwaggerResponse(400, "Error de validación en los datos de entrada.")]
    [SwaggerResponse(404, "El usuario no existe.")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateUserCommand command)
    {
        
        // Asignar el ID de la URL al comando
        var updatedCommand = command with { Id = id };
        
        // Validar que el ID de la URL coincide con el del comando
        if (id != updatedCommand.Id)
            return BadRequest("El ID del usuario no coincide con la ruta.");

        // Enviar el comando al mediador
        var result = await _mediator.Send(updatedCommand);
        
        var userUpdatedEvent = new UserUpdatedEvent
        (
            command.Email,
            new Dictionary<string, FieldChange>()
        );

        await _publishEndpoint.Publish(userUpdatedEvent);

        // Manejar el resultado según el código de estado
        return result.StatusCode switch
        {
            204 => NoContent(), // No devuelve un cuerpo de respuesta
            404 => NotFound(result.CreateResponseObject()), // Error de "no encontrado"
            _ => BadRequest(result.CreateResponseObject()) // Error genérico
        };
    }

    // DELETE: api/users/{id}
    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Elimina un usuario por Id",
        Description = "Retorna un objeto Result con 'isSuccess=true' y sin valor en caso de éxito (campo 'value' = null)."
    )]
    [SwaggerResponse(200, "Usuario eliminado correctamente.")]
    [SwaggerResponse(404, "El usuario no existe.")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteUserCommand(id);
        var result = await _mediator.Send(command);

        var userDeletedEvent = new UserDeletedEvent
        (
            command.Email,
            new Dictionary<string, FieldChange>()
        );

        await _publishEndpoint.Publish(userDeletedEvent);
        
        return result.StatusCode switch
        {
            204 => NoContent(), // No devuelve un cuerpo de respuesta
            404 => NotFound(result.CreateResponseObject()), // Error de "no encontrado"
            _ => BadRequest(result.CreateResponseObject()) // Error genérico
        };
    }
}
