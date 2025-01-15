using Refit;
using Users.Web.Domain.Abstractions;
using Users.Web.Domain.Users.Dtos;
using Users.Web.Domain.Users.Models;

namespace Users.Web.Infrastructure.Refit.Users;

public interface IRefitUsersApi
{
    [Get("/Users")]
    Task<Result<PaginatedUserDto>> GetUsers(int? page, int? size, string? sortBy);

    [Post("/Users")]
    Task<Result<Guid>> CreateUser(CreateUserModel userRequest);

    [Patch("/Users/{id}")]
    Task<Result<Guid>> UpdateUser([AliasAs("id")] Guid id, UpdateUserModel userRequest);

    [Delete("/Users/{id}")]
    Task<Result> DeleteUserById([AliasAs("id")] Guid id, string reason);
}
