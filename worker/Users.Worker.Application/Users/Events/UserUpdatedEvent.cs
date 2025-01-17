using MassTransit;
using Users.Worker.Domain.Abstractions;

namespace Users.Worker.Application.Users.Events;

[MessageUrn("UserUpdatedEventMessage")]
[EntityName("UserUpdatedEvent")]
public sealed record UserUpdatedEvent(string Email, Dictionary<string, FieldChange> ModifiedFields);
