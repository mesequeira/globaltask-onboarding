using Users.Domain.Models;

namespace Users.Application.DTOs.Users;

public class UsersPaginatedListDTO
{
    public int TotalItems { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<User> Users { get; set; }

    public UsersPaginatedListDTO()
    {
    }

    public UsersPaginatedListDTO(int totalItems, int currentPage, int pageSize, IEnumerable<User> users)
    {
        TotalItems = totalItems;
        CurrentPage = currentPage;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
        Users = users;
    }
}