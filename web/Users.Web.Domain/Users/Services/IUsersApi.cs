using Users.Web.Domain.Abstractions;
using Users.Web.Domain.Users.Dtos;
using Users.Web.Domain.Users.Models;

namespace Users.Web.Domain.Users.Services;

public interface IUsersApi
{
    Task<Result<PaginatedUserDto>> GetUsers(int? page, int? size, string? sortBy);
    Task<Result<Guid>> CreateUser(CreateUserModel userRequest);
    Task<Result> UpdateUser(Guid id, UpdateUserModel userRequest);
    Task<Result> DeleteUserById(Guid id, string reason);
}
