using PureMVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UINewProjectProxy : Proxy
{
    public UINewProjectProxy() : base(null)
    {
        ProxyName = this.GetType().ToString();
    }

    public override void OnRegister()
    {
        base.OnRegister();
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }

    public void HandleProject_Add(Notify notify)
    {
        if (notify.Feedback == FeedBack.Success)
        {
            Project_AddRsp Rsp = Project_AddRsp.Parser.ParseFrom(notify.message);
            GameFacade.SendNotification(Protocol.Project_Check, CmdType.Presenter, Rsp.Projectlist);
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter);
        }
        else
        {
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter,notify.Feedback);
        }
    }        



}

