using MassTransit;

namespace Users.Application.Users.Events;

[MessageUrn("UserDeletedEventMessage")]
[EntityName("UserDeletedEvent")]
public sealed record UserDeletedEvent(string UserName, string Email, string Reason);
