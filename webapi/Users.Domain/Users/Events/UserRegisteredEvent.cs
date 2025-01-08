namespace Users.Domain.Users.Events;

public sealed record UserRegisteredEvent(Guid Id, string UserName, string Email);
