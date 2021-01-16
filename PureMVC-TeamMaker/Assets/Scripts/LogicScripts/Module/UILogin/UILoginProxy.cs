using PureMVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILoginProxy : Proxy
{
    public UILoginProxy() : base(null)
    {
        ProxyName = this.GetType().ToString();
    }

    public void SetLoginAccount(Notify notify)
    {
        if (notify.Feedback==FeedBack.Success)
        {
            LoginRsp loginRsp = LoginRsp.Parser.ParseFrom(notify.message);
            GameFacade.RetrieveProxy<PlayerDataProxy>().SetAccount(loginRsp.Account);
            GameFacade.RetrieveProxy<PlayerDataProxy>().SetUser(loginRsp.User);

            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter, loginRsp.Account);
        }
        else
        {
            GameFacade.SendNotification(notify.Protocol, CmdType.Error, notify.Feedback);
        }
    }

    public void SetRegisterAccount(Notify notify)
    {
        if (notify.Feedback == FeedBack.Success)
        {
            RegisterRsp rsp = RegisterRsp.Parser.ParseFrom(notify.message);
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter);
        }
        else
        {
            GameFacade.SendNotification(notify.Protocol, CmdType.Error, notify.Feedback);
        }
    }

}
