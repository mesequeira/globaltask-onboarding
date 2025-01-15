using Fluxor;
using Users.Web.Domain.Abstractions;
using Users.Web.Domain.Users.Messages;
using Users.Web.Domain.Users.Models;
using Users.Web.Domain.Users.Services;
using Users.Web.Application.Users.Actions.Update;
using Users.Web.Application.Messages.Enums;
using Users.Web.Application.Messages.Actions;

namespace Users.Web.Application.Users.Effects.Update;

public sealed class UpdateUserEffects(IUsersApi usersApi)
{
    [EffectMethod]
    public async Task HandleUpdateUserAction(UpdateUserAction updateUserAction, IDispatcher dispatcher)
    {
        string message = string.Empty;
        MessageType messageType = MessageType.Success;

        try
        {
            UpdateUserModel userModel = updateUserAction.UpdateUserModel;

            Result result = await usersApi.UpdateUser(updateUserAction.Id, userModel);

            if (result.IsSuccess)
            {
                var action = new UpdateUserResultAction(updateUserAction.Id, userModel.Email, userModel.PhoneNumber);

                dispatcher.Dispatch(action);

                message = UserMessages.UserUpdatedSuccesfully;
            }
            else
            {
                string? errorDescription = result.Errors.FirstOrDefault()?.Description;
                message = errorDescription ?? UserMessages.UserUpdatedError;
                messageType = MessageType.Error;
            }
        }
        catch
        {
            message = UserMessages.UserUpdatedError;
            messageType = MessageType.Error;
        }
        finally
        {
            dispatcher.Dispatch(new CreateMessageAction(message, messageType));
        }
    }
}
