using Users.Web.Domain.DTO;
using Users.Web.Domain.Models;

namespace Users.Web.Domain.Services
{
    public interface IUserService
    {
        Task<List<UserDTO>?> GetUsersAsync(int page = 1, int size = 25, string sortBy = "name");
    }
}