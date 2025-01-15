using Fluxor;
using Fluxor.Blazor.Web.Components;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Users.Web.Application.Messages.Enums;
using Users.Web.Application.Messages.States;

namespace Users.Web.Manegement.Components.Providers;

public partial class MessagesProvider : FluxorComponent
{
    [Inject]
    public required IState<MessagesState> State { get; set; }

    [Inject]
    public required ISnackbar _snackbar { get; set; }

    protected override void OnAfterRender(bool firstRender)
    {
        base.OnAfterRender(firstRender);
        _snackbar.Configuration.PositionClass = Defaults.Classes.Position.BottomLeft;

        string message = State.Value.Message;

        switch (State.Value.Type)
        {
            case MessageType.Success:
                _snackbar.Add(message, Severity.Success);
                break;

            case MessageType.Error:
                _snackbar.Add(message, Severity.Error);
                break;
        }
    }
}