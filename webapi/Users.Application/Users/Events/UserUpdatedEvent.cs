using MassTransit;
using Users.Domain.Abstractions;

namespace Users.Application.Users.Events;

[MessageUrn("UserUpdatedEventMessage")]
[EntityName("UserUpdatedEvent")]
public sealed record UserUpdatedEvent(string Email, Dictionary<string, FieldChange> ModifiedFields);
