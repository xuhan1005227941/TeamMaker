using Google.Protobuf;
using PureMVC;
using PureMVC.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UINewProjectCommand : MacroCommand
{
    protected override void InitializeMacroCommand()
    {
        AddSubCommand(delegate { return new UINewProjectRequestCommand(); });
        AddSubCommand(delegate { return new UINewProjectResponseCommand(); });
    }

}

public class UINewProjectRequestCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        int mainproto = int.Parse(notification.Name);
        CmdType type = (CmdType)Enum.Parse(typeof(CmdType), notification.Type);
        if (type == CmdType.Request)
        {
            Message message = notification.Body as Message;
            Notify notify = new Notify();
            notify.Protocol = mainproto;
            switch (mainproto)
            {
                case Protocol.Project_Add:
                    Project_AddReq addReq = ReferencePool.Require<Project_AddReq>();
                    addReq.ProjectName = message.args[0];
                    addReq.Priority = int.Parse(message.args[1]);
                    addReq.Description = message.args[2];
                    addReq.CreateDate = message.args[3];
                    addReq.LimitDate = message.args[4];
                    notify.message = addReq.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;

            }
        }
    }
}

public class UINewProjectResponseCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        int mainproto = int.Parse(notification.Name);
        CmdType type = (CmdType)Enum.Parse(typeof(CmdType), notification.Type);
        Notify notify = notification.Body as Notify;
        if (type==CmdType.Response)
        {
            switch (mainproto)
            {
                case Protocol.Project_Add:
                    GameFacade.RetrieveProxy<UINewProjectProxy>().HandleProject_Add(notify);
                    break;                                   
             
            }
        }
        
    }

   
}

