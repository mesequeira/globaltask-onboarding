using Users.Web.Domain.DTO;

namespace Users.Web.Domain.Models.APi;

public class PaginatedUsersResponse
{
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public List<UserDTO> Users { get; set; } = new();
}