using MediatR;
using Microsoft.AspNetCore.Mvc;
using Users.Application.Users.Commands.Create;
using Users.Application.Users.Commands.Delete;
using Users.Application.Users.Commands.Update;
using Users.Application.Users.Dtos;
using Users.Application.Users.Queries.GetAll;
using Users.Application.Users.Queries.GetById;
using Users.Domain.Abstractions;
using Users.WebApi.Extensions;
using Users.WebApi.Users.Dtos;

namespace Users.WebApi.Users.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController(ISender _sender) : ControllerBase
{
    /// <summary>
    /// Crea un nuevo usuario.
    /// </summary>
    /// <param name="userRequest">Los detalles del usuario a crear.</param>
    /// <returns>Crea un usuario y retorna el Id asignado en el campo "value".</returns>
    /// <response code="201">Usuario creado exitosamente.</response>
    /// <response code="400">Solicitud incorrecta. Error en la validación de los datos de entrada.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest userRequest, CancellationToken cancellationToken = default)
    {
        CreateUserCommand userCommand = new(userRequest.UserName, 
            userRequest.Email, 
            userRequest.Password, 
            userRequest.PhoneNumber, 
            userRequest.Birthday);

        Result<Guid> result = await _sender.Send(userCommand, cancellationToken);

        return this.GetActionResult(result);
    }

    ///<summary>
    ///Obtiene un listado paginado de usuarios.
    /// </summary>
    /// <param name="page">Número de página.</param>
    /// <param name="size">Cantidad de registros por página.</param>
    /// <param name="sortBy">Campo por el cual se ordenará la búsqueda.</param>
    /// <returns>Retorna la lista de usuarios paginada en el campo "value".</returns>
    /// <response code="200">Lista de usuarios paginada.</response>
    /// <response code="400">Párametros de paginación inválidos.</response>
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetUsers(
        int page = 1, 
        int size = 25, 
        string sortBy = "UserName", 
        CancellationToken cancellationToken = default
        )
    {
        GetUsersQuery query = new(page, size, sortBy);

        Result<PaginatedUserDto> result = await _sender.Send(query, cancellationToken);

        return this.GetActionResult(result);
    }

    ///<summary>
    ///Obtiene la información de un usuario por id.
    /// </summary>
    /// <param name="id">Identificador del usuario.</param>
    /// <returns>Retorna la información del usuario en el campo "value".</returns>
    /// <response code="200">Datos del usuario.</response>
    /// <response code="404">El usuario no existe.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserById(Guid id, CancellationToken cancellationToken = default)
    {
        GetUserByIdQuery query = new(id);

        Result<UserResponseDto> result = await _sender.Send(query, cancellationToken);

        return this.GetActionResult(result);
    }

    /// <summary>
    /// Actualiza un usuario.
    /// </summary>
    /// <param name="id">Identificador del usuario.</param>
    /// <param name="userRequest">Los datos para actualizar el usuario.</param>
    /// <returns>Retorna un objeto Result con "isSuccess=true" y sin valor en caso de éxito (campo "value" = null).</returns>
    /// <response code="200">Usuario actualizado exitosamente.</response>
    /// <response code="400">Solicitud incorrecta. Error en la validación de los datos de entrada.</response>
    /// <response code="404">El usuario no existe.</response>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)] 
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest userRequest, CancellationToken cancellationToken = default)
    {
        UpdateUserCommand userCommand = new(
            id,
            userRequest.Password,
            userRequest.PhoneNumber,
            userRequest.Email);

        Result result = await _sender.Send(userCommand, cancellationToken);

        return this.GetActionResult(result);
    }

    /// <summary>
    /// Elimina un usuario por id.
    /// </summary>
    /// <param name="id">Identificador del usuario.</param>
    /// <returns>Retorna un objeto Result con "isSuccess=true" y sin valor en caso de éxito (campo "value" = null).</returns>
    /// <response code="200">Usuario eliminado exitosamente.</response>
    /// <response code="404">El usuario no existe.</response>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUser(Guid id, CancellationToken cancellationToken = default)
    {
        DeleteUserCommand command = new(id);

        Result result = await _sender.Send(command, cancellationToken);

        return this.GetActionResult(result);
    }
}
