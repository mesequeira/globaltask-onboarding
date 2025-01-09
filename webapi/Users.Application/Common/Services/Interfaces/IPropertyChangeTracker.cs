using Users.Domain.Abstractions;

namespace Users.Application.Common.Services.Interfaces;

public interface IPropertyChangeTracker<TModel, TCommand>
{
    Dictionary<string, FieldChange> GetChanges(TModel entity, TCommand command);
}
