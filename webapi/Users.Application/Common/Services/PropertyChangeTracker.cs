using System.Reflection;
using Users.Application.Common.Services.Interfaces;
using Users.Domain.Abstractions;

namespace Users.Application.Common.Services;

public sealed class PropertyChangeTracker<TModel, TCommand> : IPropertyChangeTracker<TModel, TCommand>
{
    public Dictionary<string, FieldChange> GetChanges(TModel entity, TCommand command)
    {
        Dictionary<string, FieldChange> changes = new();

        PropertyInfo[] entityProps = typeof(TModel).GetProperties();
        PropertyInfo[] commandProps = typeof(TCommand).GetProperties();

        foreach (PropertyInfo commandPropertyInfo in commandProps)
        {
            PropertyInfo? entityPropertyInfo = entityProps.FirstOrDefault(p => p.Name == commandPropertyInfo.Name);

            if (entityPropertyInfo == null)
                continue;

            object? originalValue = entityPropertyInfo.GetValue(entity);
            object? updatedValue = commandPropertyInfo.GetValue(command);

            if (!Equals(originalValue, updatedValue))
            {
                changes.Add(
                    entityPropertyInfo.Name, 
                    new FieldChange(originalValue?.ToString(), updatedValue?.ToString())
                );
            }
        }

        return changes;
    }
}
