using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using Users.Application.Users.Commands;
using Users.Application.Users.Dtos;
using Users.Application.Users.Queries;
using Users.Domain.Abstractions;
using Users.WebApi.Users.Dtos;

namespace Users.WebApi.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;
        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserRequest userRequest)
        {
            CreateUserCommand userCommand = new(userRequest.UserName, 
                userRequest.Email, 
                userRequest.Password, 
                userRequest.PhoneNumber, 
                userRequest.Birthday);

            Result<Guid> result = await _sender.Send(userCommand);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }


        [HttpPatch("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserRequest userRequest)
        {
            UpdateUserCommand userCommand = new(
                id,
                userRequest.Email,
                userRequest.Password,
                userRequest.PhoneNumber);

            Result result = await _sender.Send(userCommand);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(int page = 1, int size = 25, string sortBy = "UserName")
        {
            GetUsersQuery query = new(page, size, sortBy);

            Result<IEnumerable<UserResponseDto>> result = await _sender.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            GetUserByIdQuery query = new(id);

            Result<UserResponseDto> result = await _sender.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            DeleteUserCommand command = new(id);

            Result result = await _sender.Send(command);

            if (result.IsSuccess)
            {
                return NoContent();
            }
            else
            {
                return BadRequest(result);
            }
        }
    }
}
