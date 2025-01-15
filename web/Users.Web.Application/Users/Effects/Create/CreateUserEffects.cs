using Fluxor;
using Users.Web.Application.Messages.Actions;
using Users.Web.Application.Messages.Enums;
using Users.Web.Application.Users.Actions.Create;
using Users.Web.Domain.Abstractions;
using Users.Web.Domain.Users;
using Users.Web.Domain.Users.Messages;
using Users.Web.Domain.Users.Models;
using Users.Web.Domain.Users.Services;

namespace Users.Web.Application.Users.Effects.Create;

public class CreateUserEffects(IUsersApi usersApi)
{
    [EffectMethod]
    public async Task HandleCreateUserAction(CreateUserAction createUserAction, IDispatcher dispatcher)
    {
        string message = string.Empty;
        MessageType messageType = MessageType.Success;

        try
        {
            CreateUserModel userModel = createUserAction.CreateUserModel;

            Result<Guid> result = await usersApi.CreateUser(userModel);

            if (result.IsSuccess)
            {
                UserResponseDto user = new UserResponseDto()
                {
                    Id = result.Value,
                    Birthday = userModel.Birthday!.Value,
                    PhoneNumber = userModel.PhoneNumber,
                    Email = userModel.Email,
                    UserName = userModel.UserName,
                };

                dispatcher.Dispatch(new CreateUserResultAction(user));

                message = UserMessages.UserCreatedSuccesfully;
            }
            else
            {
                string? errorDescription = result.Errors.FirstOrDefault()?.Description;
                message = errorDescription ?? UserMessages.UserCreatedError;
                messageType = MessageType.Error;
            }
        }
        catch
        {
            message = UserMessages.UserCreatedError;
            messageType = MessageType.Error;
        }
        finally 
        {
            dispatcher.Dispatch(new CreateMessageAction(message, messageType));
        }
    }
}
