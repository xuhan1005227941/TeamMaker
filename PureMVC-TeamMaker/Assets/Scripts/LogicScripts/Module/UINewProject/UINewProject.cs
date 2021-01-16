using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UINewProject : IUI
{

    public void OnCreate()
    {
        GameFacade.RegisterCommand<UINewProjectCommand>(Protocol.Project_Add);
        GameFacade.RegisterProxy<UINewProjectProxy>();
        GameFacade.RegisterMediator<UINewProjectMediator>();
    }

    public void OnClose()
    {
        GameFacade.RemoveCommand(Protocol.Project_Add);
        GameFacade.RemoveProxy<UINewProjectProxy>();
        GameFacade.RemoveMediator<UINewProjectMediator>();
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
