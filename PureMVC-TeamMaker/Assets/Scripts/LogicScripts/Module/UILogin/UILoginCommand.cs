using Google.Protobuf;
using PureMVC;
using PureMVC.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UILoginCommand:MacroCommand
{
    protected override void InitializeMacroCommand()
    {
        AddSubCommand(delegate { return new UILoginRequestCommand(); });
        AddSubCommand(delegate { return new UILoginResponseCommand(); });
    }

}

public class UILoginRequestCommand : SimpleCommand
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
                case Protocol.Login:
                    LoginReq loginReq = ReferencePool.Require<LoginReq>();
                    loginReq.Account = message.args[0].ToString();
                    loginReq.Password = message.args[1].ToString();
                    notify.message = loginReq.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;
                case Protocol.Regist:
                    RegisterReq registerReq = ReferencePool.Require<RegisterReq>();
                    registerReq.Account = message.args[0].ToString();
                    registerReq.Password = message.args[1].ToString();
                    notify.message = registerReq.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;
            }
        }
    }
}

public class UILoginResponseCommand : SimpleCommand
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
                case Protocol.Login:
                    GameFacade.RetrieveProxy<UILoginProxy>().SetLoginAccount(notify);
                    break;
                case Protocol.Regist:
                    GameFacade.RetrieveProxy<UILoginProxy>().SetRegisterAccount(notify);
                    break;
            }
        }
    }

}

