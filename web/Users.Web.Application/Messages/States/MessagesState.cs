using Fluxor;
using Users.Web.Application.Messages.Enums;

namespace Users.Web.Application.Messages.States;

[FeatureState]
public sealed class MessagesState
{
    public string Message { get; }
    public MessageType Type { get; }
    private MessagesState() { }
    public MessagesState(string message, MessageType type)
    {
        Message = message;
        Type = type;
    }
}
