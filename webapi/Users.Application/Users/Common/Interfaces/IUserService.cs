using Application.Common.Models;
using Application.Users.Commands.CreateUser;
using Application.Users.Commands.UpdateUser;
using Application.Users.Queries;

namespace Application.Users.Services;

public interface IUserService
{
    Task<int> CreateUserAsync(CreateUserCommand command, CancellationToken cancellationToken);
    Task UpdateUserAsync(UpdateUserCommand command, CancellationToken cancellationToken);
    Task DeleteUserAsync(int userId, CancellationToken cancellationToken);
    Task<UserDto> GetUserByIdAsync(int userId, CancellationToken cancellationToken);
    Task<PaginatedList<UserDto>> GetAllUsersAsync(int page, int pageSize, CancellationToken cancellationToken);
}
