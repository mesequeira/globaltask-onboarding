namespace Users.Web.Domain.Users.Models;

public sealed class CreateUserModel
{
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public DateTime? Birthday { get; set; } = DateTime.UtcNow;
}
