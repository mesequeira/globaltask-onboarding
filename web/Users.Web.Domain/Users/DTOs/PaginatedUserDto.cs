namespace Users.Web.Domain.Users.Dtos;

public sealed class PaginatedUserDto
{
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int Page {  get; set; }
    public List<UserResponseDto> Data { get; set; } = new();
}
