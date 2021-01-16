
public class UILogin : IUI
{
    public void OnCreate()
    {
        GameFacade.RegisterCommand<UILoginCommand>(Protocol.Login,Protocol.Regist);
        GameFacade.RegisterProxy<UILoginProxy>(); 
        GameFacade.RegisterProxy<PlayerDataProxy>();
        GameFacade.RegisterMediator<UILoginMediator>();
    }

    public void OnClose()
    {
        GameFacade.RemoveCommand(Protocol.Login,Protocol.Regist);
        GameFacade.RemoveProxy<UILoginProxy>();
        GameFacade.RemoveMediator<UILoginMediator>();
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
