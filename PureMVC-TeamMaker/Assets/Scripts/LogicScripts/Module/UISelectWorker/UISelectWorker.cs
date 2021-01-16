using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UISelectWorker : IUI
{
    
    public void OnCreate()
    {
        GameFacade.RegisterCommand<UISelectWorkerCommand>(Protocol.Friend_Check);
        GameFacade.RegisterMediator<UISelectWorkerMediator>();
        GameFacade.RegisterProxy<UISelectWorkerProxy>();
    }
    public void OnClose()
    {
        GameFacade.RemoveCommand(Protocol.Friend_Check);
        GameFacade.RemoveMediator<UISelectWorkerMediator>();
        GameFacade.RemoveProxy<UISelectWorkerProxy>();
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
