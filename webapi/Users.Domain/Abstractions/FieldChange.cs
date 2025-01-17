namespace Users.Worker.Domain.Abstractions;

public sealed record FieldChange(string? OldValue, string? NewValue);
