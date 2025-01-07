namespace Users.Application.Users.Dtos;

public sealed class PaginatedUserDto
{
    public PaginatedUserDto(int totalItems, int pageSize, int page, List<UserResponseDto> data) 
    { 
        TotalItems = totalItems;
        PageSize = pageSize;
        Page = page;
        Data = data;
    }
    public int TotalItems { get; set; }
    public int PageSize { get; set; }
    public int Page {  get; set; }
    public List<UserResponseDto> Data { get; set; } = new();
}
