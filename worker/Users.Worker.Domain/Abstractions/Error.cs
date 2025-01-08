namespace Users.Worker.Domain.Abstractions;

public class Error(string code, string? description = null)
{
    public string Code { get; set; } = code!;
    public string? Description { get; set; } = description!;

    public static readonly Error None = new(string.Empty);
}
