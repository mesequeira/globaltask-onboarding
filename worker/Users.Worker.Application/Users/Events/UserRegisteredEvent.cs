using MassTransit;

namespace Users.Worker.Application.Users.Events;

[MessageUrn("UserRegisteredEventMessage")]
[EntityName("UserRegisteredEvent")]
public sealed record UserRegisteredEvent(Guid Id, string UserName, string Email);
