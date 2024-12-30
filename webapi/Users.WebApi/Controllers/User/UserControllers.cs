using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Users.WebApi.Controllers.User;

[ApiController]
[Route("api/users")]
public class UserControllers : ControllerBase
{
    
    [HttpPost()]
    // [SwaggerOperation(
    //     Summary = "Crea un nuevo Usuario",
    //     Description = "Crea un usuario y retorna el Id asignado en el campo \"value\"."
    // )]
    // [SwaggerResponse(200, "El usuario fue creado correctamente.")]
    // [SwaggerResponse(400, "Error de validación en los datos de entrada.")]
    public IActionResult PostUser([FromBody] string message)
    {
        return Ok(new { Echo = message });
    }
    
    [HttpGet()]
    [SwaggerOperation(
        Summary = "Obtiene un listado paginado de usuarios",
        Description = "Retorna la lista de usuarios paginada en el campo \"value\"."
    )]
    [SwaggerResponse(200, "Lista de usuarios paginada.", typeof(object))] // Specify response type
    [SwaggerResponse(400, "Parámetros de paginación inválidos.")]
    public IActionResult GetAllUsers()
    {
        return Ok(new { Message = "Hello, World!" });
    }
    
    [HttpGet("{id:int}")]
    public IActionResult GetUserById(int id)
    {
        return Ok(new { Message = "Hello, World!" });
    }
    
    [HttpPatch("{id:int}")]
    public IActionResult UpdateUser(int id)
    {
        return Ok(new { Message = "Hello, World!" });
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult DeleteUser(int id)
    {
        return Ok(new { Message = "Hello, World!" });
    }

    
    
}