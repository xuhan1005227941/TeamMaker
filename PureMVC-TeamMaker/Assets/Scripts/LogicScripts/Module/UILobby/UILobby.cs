
public class UILobby : IUI
{
    public void OnCreate()
    {
        GameFacade.RegisterCommand<UILobbyCommand>(Protocol.Project_Check);
        GameFacade.RegisterProxy<UILobbyProxy>();
        GameFacade.RegisterMediator<UILobbyMediator>();
    }

    public void OnClose()
    {
        GameFacade.RemoveCommand(Protocol.Project_Check);
        GameFacade.RemoveProxy<UILobbyProxy>();
        GameFacade.RemoveMediator<UILobbyMediator>();
    }

    public void OnFrozen(bool isF)
    {
        if (isF)
        {

        }
        else
        {

        }
    }
 

}
