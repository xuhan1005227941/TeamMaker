using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIUsermessage : IUI
{

    public void OnCreate()
    {
        GameFacade.RegisterCommand<UIUsermessageCommand>(Protocol.UserMessage_Update);
        GameFacade.RegisterMediator<UIUserMessageMediator>();
    }
    public void OnClose()
    {
        GameFacade.RemoveCommand(Protocol.UserMessage_Update);
        GameFacade.RemoveMediator<UIUserMessageMediator>();
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
