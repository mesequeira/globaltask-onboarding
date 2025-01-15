using Users.Web.Domain.Abstractions;
using Users.Web.Domain.Users.Dtos;
using Users.Web.Domain.Users.Models;
using Users.Web.Domain.Users.Services;
using Users.Web.Infrastructure.Refit.Users;

namespace Users.Web.Infrastructure.Users.Services;

public sealed class UsersApi(IRefitUsersApi refitClient) : IUsersApi
{
    public async Task<Result<Guid>> CreateUser(CreateUserModel userRequest)
    {
       return await refitClient.CreateUser(userRequest);
    }

    public async Task<Result> DeleteUserById(Guid id, string reason)
    {
        return await refitClient.DeleteUserById(id, reason);
    }

    public async Task<Result<PaginatedUserDto>> GetUsers(int? page, int? size, string? sortBy)
    {
        return await refitClient.GetUsers(page, size, sortBy);
    }

    public async Task<Result> UpdateUser(Guid id, UpdateUserModel userRequest)
    {
        return await refitClient.UpdateUser(id, userRequest);
    }
}
