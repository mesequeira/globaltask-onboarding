using Users.Worker.Domain.Abstractions;

namespace Users.Worker.Domain.Users.Notifications;

public static class UserEmailMessages
{
    public static EmailMessage UserRegistered(string userName)
        => new("Welcome!", $"Hi! {userName} we are very happy to have you here!");

    public static EmailMessage UserDeleted(string userName, string reason)
        => new("Your user has been deleted.", $"Hi! {userName} your account has been deleted.\n Reason: reason");

    public static EmailMessage UserUpdated(Dictionary<string, FieldChange> modifiedFields)
    {
        string subject = $"User Updated";
        string body = $"Your user has been updated.\n\n";

        foreach (KeyValuePair<string, FieldChange> field in modifiedFields)
        {
            body += $"{field.Key}: {field.Value.OldValue} -> {field.Value.NewValue}\n";
        }

        return new(subject, body);
    }
}