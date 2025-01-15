using Fluxor;
using Users.Web.Application.Messages.Actions;
using Users.Web.Application.Messages.States;

namespace Users.Web.Application.Messages.Reducers;

public static class MessageReducers
{
    [ReducerMethod]
    public static MessagesState ReduceCreateMessageAction(MessagesState state, CreateMessageAction action) =>
       new MessagesState(
         message: action.Message,
         type: action.Type
       );
}
