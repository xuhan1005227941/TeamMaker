using Google.Protobuf;
using PureMVC;
using PureMVC.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UILobbyCommand : MacroCommand
{
    protected override void InitializeMacroCommand()
    {
        AddSubCommand(delegate { return new UILobbyRequestCommand(); });
        AddSubCommand(delegate { return new UILobbyResponseCommand(); });
    }
}

public class UILobbyRequestCommand:SimpleCommand
{
    public override void Execute(INotification notification)
    {
        int mainproto = int.Parse(notification.Name);
        CmdType type = (CmdType)int.Parse(notification.Type);
        Message message = notification.Body as Message;
        Notify notify = new Notify();
        notify.Protocol = mainproto;
        if (type == CmdType.Request)
        {
            switch (mainproto)
            {
                case Protocol.Project_Check:
                    Project_CheckReq checkReq = ReferencePool.Require<Project_CheckReq>();
                    checkReq.Empty = 0;
                    notify.message = checkReq.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;
                case Protocol.Project_Add:
                    Project_AddReq addReq = ReferencePool.Require<Project_AddReq>();
                    addReq.ProjectName = message.args[0].ToString();
                    addReq.Priority = int.Parse(message.args[1]);
                    addReq.Description = message.args[2].ToString();
                    addReq.CreateDate = message.args[3].ToString();
                    addReq.LimitDate = message.args[4].ToString();
                    notify.message = addReq.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;
                case Protocol.Project_Delete:
                    Project_DeleteReq deleteReq = ReferencePool.Require<Project_DeleteReq>();
                    deleteReq.ProjectID = int.Parse(message.args[0]);
                    notify.message = deleteReq.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;
            }
        }
    }
}

public class UILobbyResponseCommand:SimpleCommand
{
    public override void Execute(INotification notification)
    {
        int mainproto = int.Parse(notification.Name);
        CmdType type = (CmdType)int.Parse(notification.Type);
        Notify notify = notification.Body as Notify;
        if (type == CmdType.Response)
        {
            switch (mainproto)
            {
                case Protocol.Project_Check:
                    GameFacade.RetrieveProxy<UILobbyProxy>().SetProjectCheck(notify); break;
                case Protocol.Project_Add:
                    GameFacade.RetrieveProxy<UILobbyProxy>().SetProjectAdd(notify); break;
                case Protocol.Project_Delete:
                    GameFacade.RetrieveProxy<UILobbyProxy>().SetProjectDelete(notify); break;
            }
        }
    }
}
