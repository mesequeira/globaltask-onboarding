using Application.Common.Models;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries;
using Application.Users.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Users.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateUserCommand>(request);

        var validator = new CreateUserCommandValidator();
        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var userId = await _userService.CreateUserAsync(command, cancellationToken);
        return Ok(Result<int>.Success(userId, StatusCodes.Status201Created, "Usuario creado exitosamente."));
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateUserCommand
        {
            Id = id,
            Name = request.Name,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            Birthday = request.Birthday
        };

        var validator = new UpdateUserCommandValidator();
        var validationResult = validator.Validate(command);

        if (!validationResult.IsValid)
        {
            return BadRequest(new
            {
                message = "Errores de validación.",
                errors = validationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
            });
        }

        await _userService.UpdateUserAsync(command, cancellationToken);

        return Ok(new { message = "Usuario actualizado correctamente." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
    {
        await _userService.DeleteUserAsync(id, cancellationToken);

        return Ok(new { message = "Usuario eliminado correctamente." });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
    {
        var user = await _userService.GetUserByIdAsync(id, cancellationToken);
        return Ok(Result<UserDto>.Success(user, StatusCodes.Status200OK));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken, int page = 1, int pageSize = 25)
    {
        var users = await _userService.GetAllUsersAsync(page, pageSize, cancellationToken);
        return Ok(Result<PaginatedList<UserDto>>.Success(users, StatusCodes.Status200OK));
    }
}
