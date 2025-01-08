namespace Users.Worker.Domain.Users.Events;
public sealed record UserRegisteredEvent(Guid Id, string UserName, string Email);
