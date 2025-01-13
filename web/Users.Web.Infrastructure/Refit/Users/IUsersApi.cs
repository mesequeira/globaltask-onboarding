using Refit;
using Users.Web.Domain.Abstractions;
using Users.Web.Domain.Users.Dtos;

namespace Users.Web.Infrastructure.Refit.Users;

public interface IUsersApi
{
    [Get("/Users")]
    Task<Result<PaginatedUserDto>> GetUsers(int? page, int? size, string? sortBy); 
}
