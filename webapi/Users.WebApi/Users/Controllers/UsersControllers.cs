using Application.Common.Models;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Users.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UsersController(IMapper mapper, IMediator mediator)
    {
        _mapper = mapper;
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateUserCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(new { message = "Usuario creado correctamente.", id = result });
    }

    [HttpPatch("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateUserCommand>(request);
        command.Id = id; 

        await _mediator.Send(command, cancellationToken);
        return Ok(new { message = "Usuario actualizado correctamente." });
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteUserCommand { Id = id };
        await _mediator.Send(command, cancellationToken);
        return Ok(new { message = "Usuario eliminado correctamente." });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery { Id = id };
        var user = await _mediator.Send(query, cancellationToken);
        return Ok(Result<UserDto>.Success(user, StatusCodes.Status200OK));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers(CancellationToken cancellationToken, int page = 1, int pageSize = 25)
    {
        var query = new GetAllUsersQuery { Page = page, PageSize = pageSize };
        var users = await _mediator.Send(query, cancellationToken);
        return Ok(Result<PaginatedList<UserDto>>.Success(users, StatusCodes.Status200OK));
    }
}
