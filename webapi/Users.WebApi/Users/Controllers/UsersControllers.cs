using Application.Common.Models;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.DeleteUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Users.Application.Users.Commands.Queries;

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
    public async Task<Result<int>> CreateUser([FromBody] CreateUserCommand command, CancellationToken cancellationToken)
    {
        return await _mediator.Send(command, cancellationToken);
    }


    [HttpPatch("{id}")]
    public async Task<Result<Unit>> UpdateUser(int id, [FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
    {
        return await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete("{id}")]
    public async Task<Result<Unit>> DeleteUser(int id, [FromBody] DeleteUserCommand command, CancellationToken cancellationToken)
    {
        //command.Id = id;
        return await _mediator.Send(command, cancellationToken);
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
