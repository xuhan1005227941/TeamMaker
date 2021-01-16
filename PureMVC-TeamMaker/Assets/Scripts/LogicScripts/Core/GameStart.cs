using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC;

public class GameStart : MonoBehaviour
{
    void Start()
    {
        GameFacade.Install();
        GameFacade.RegisterCommand<StartCmd>(CMD.GameStart);
        GameFacade.SendNotification(CMD.GameStart);
        GameFacade.RemoveCommand(CMD.GameStart);
    }

}