using Fluxor;
using Users.Web.Application.Abstract;
using Users.Web.Application.Messages.Actions;
using Users.Web.Application.Messages.Enums;
using Users.Web.Application.Users.Actions.Get;
using Users.Web.Domain.Abstractions;
using Users.Web.Domain.Users.Dtos;
using Users.Web.Domain.Users.Messages;
using Users.Web.Domain.Users.Services;

namespace Users.Web.Application.Users.Effects.Get;

public class FetchUsersEffects(IUsersApi usersApi)
{
    [EffectMethod(typeof(FetchUsersAction))]
    public async Task HandleFetchDataAction(IDispatcher dispatcher)
    {
        string message = string.Empty;
        MessageType messageType = MessageType.Error;
        try
        {
            Result<PaginatedUserDto> result = await usersApi.GetUsers(null, null, null);

            if (result.IsSuccess)
            {
                dispatcher.Dispatch(new FetchUsersResultAction(result.Value!.Data));
            }
            else
            {
                string? errorDescription = result.Errors.FirstOrDefault()?.Description;

                message = errorDescription ?? "Algo salió mal, intentalo de nuevo más tarde.";
            }
        }
        catch
        {
            message = UserMessages.GetUsersError;
        }
        finally
        {
            dispatcher.Dispatch(new CreateMessageAction(message, messageType));
        }
    }

}
