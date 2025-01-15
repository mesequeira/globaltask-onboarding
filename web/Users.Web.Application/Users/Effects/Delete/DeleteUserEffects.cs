using Fluxor;
using Users.Web.Application.Messages.Actions;
using Users.Web.Application.Messages.Enums;
using Users.Web.Application.Users.Actions.Delete;
using Users.Web.Domain.Abstractions;
using Users.Web.Domain.Users.Messages;
using Users.Web.Domain.Users.Services;

namespace Users.Web.Application.Users.Effects.Delete;

public sealed class DeleteUserEffects(IUsersApi usersApi)
{
    [EffectMethod]
    public async Task HandleUpdateUserAction(DeleteUserAction deleteUserAction, IDispatcher dispatcher)
    {
        string message = string.Empty;
        MessageType messageType = MessageType.Success;

        try
        {
            Result result = await usersApi.DeleteUserById(deleteUserAction.Id, deleteUserAction.Reason);

            if (result.IsSuccess)
            {
                var action = new DeleteUserResultAction(deleteUserAction.Id);

                dispatcher.Dispatch(action);

                message = UserMessages.UserDeletedSuccesfully;
            }
            else
            {
                string? errorDescription = result.Errors.FirstOrDefault()?.Description;
                message = errorDescription ?? UserMessages.UserDeletedError;
                messageType = MessageType.Error;
            }
        }
        catch
        {
            message = UserMessages.UserDeletedError;
            messageType = MessageType.Error;
        }
        finally
        {
            dispatcher.Dispatch(new CreateMessageAction(message, messageType));
        }
    }
}
