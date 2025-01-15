using Users.Web.Application.Messages.Enums;

namespace Users.Web.Application.Messages.Actions;

public record CreateMessageAction(string Message, MessageType Type);
