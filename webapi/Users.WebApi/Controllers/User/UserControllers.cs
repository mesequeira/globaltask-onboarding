using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Users.Application.Users.Commands.CreateUser;
using Users.Application.Users.Commands.DeleteUser;
using Users.Application.Users.Commands.UpdateUser;
using Users.Application.Users.Queries.GetAllUsers;
using Users.Application.Users.Queries.GetUserById;
using Users.Domain.Models;


namespace Users.WebApi.Controllers.User;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    
    private readonly IMediator _mediator;
    
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    // POST: api/users
    [HttpPost]
    [SwaggerOperation(
        Summary = "Crea un nuevo usuario",
        Description = "Crea un usuario y retorna el Id asignado en el campo 'value'."
    )]
    [SwaggerResponse(201, "El usuario fue creado correctamente.", typeof(Guid))]
    [SwaggerResponse(400, "Error de validación en los datos de entrada.")]
    public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
    {
        var createdUserId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetById), new { id = createdUserId }, createdUserId);
    }

    // GET: api/users
    [HttpGet]
    [SwaggerOperation(
        Summary = "Obtiene un listado paginado de usuarios",
        Description = "Retorna la lista de usuarios paginada en el campo 'value'."
    )]
    [SwaggerResponse(200, "Lista de usuarios paginada.", typeof(IEnumerable<Domain.Models.User>))]
    [SwaggerResponse(400, "Parámetros de paginación inválidos.")]
    public async Task<IActionResult> GetUsers(
        [FromQuery] int page = 1,
        [FromQuery] int size = 25,
        [FromQuery] string sortBy = "name")
    {
        var query = new GetAllUsersQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    // GET: api/users/{id}
    [HttpGet("{id:int}")]
    [SwaggerOperation(
        Summary = "Obtiene la información de un usuario por Id",
        Description = "Retorna la información del usuario en el campo 'value'."
    )]
    [SwaggerResponse(200, "Datos del usuario.")]
    [SwaggerResponse(404, "No se encontró un usuario con ese Id.")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var query = new GetUserByIdQuery(id);
        var result = await _mediator.Send(query);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    // PATCH: api/users/{id}
    [HttpPatch("{id:int}")]
    [SwaggerOperation(
        Summary = "Actualiza los datos de un usuario",
        Description = "Retorna un objeto Result con 'isSuccess=true' y sin valor en caso de éxito (campo 'value' = null)."
    )]
    [SwaggerResponse(200, "Usuario actualizado correctamente.")]
    [SwaggerResponse(400, "Error de validación en los datos de entrada.")]
    [SwaggerResponse(404, "El usuario no existe.")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserCommand command)
    {
        if (id != command.Id)
            return BadRequest("El ID del usuario no coincide con la ruta.");

        var success = await _mediator.Send(command);
        if (!success)
            return NotFound();

        return NoContent();
    }

    // DELETE: api/users/{id}
    [HttpDelete("{id:int}")]
    [SwaggerOperation(
        Summary = "Elimina un usuario por Id",
        Description = "Retorna un objeto Result con 'isSuccess=true' y sin valor en caso de éxito (campo 'value' = null)."
    )]
    [SwaggerResponse(200, "Usuario eliminado correctamente.")]
    [SwaggerResponse(404, "El usuario no existe.")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var command = new DeleteUserCommand(id);
        var success = await _mediator.Send(command);

        if (!success)
            return NotFound();

        return NoContent();
    }
}
