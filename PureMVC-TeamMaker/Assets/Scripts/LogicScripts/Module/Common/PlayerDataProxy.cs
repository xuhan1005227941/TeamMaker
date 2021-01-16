using PureMVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDataProxy:Proxy
{
    public PlayerDataProxy() : base(null)
    {
        ProxyName = this.GetType().ToString();
    }

    public AccountModel account { get; private set; }
    public UserMessageModel user { get; private set; }

    public void SetAccount(AccountModel account)
    {
        this.account = account;
    }
    public void SetUser(UserMessageModel user)
    {
        this.user = user;
    }
    public void UpdateUser(Notify notify)
    {
        if (notify.Feedback == FeedBack.Success)
        {
            UserMessage_UpdateRsp updateUser = UserMessage_UpdateRsp.Parser.ParseFrom(notify.message);
            this.user = updateUser.User;
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter, this.user);
        }
        else
        {
            GameFacade.SendNotification(notify.Protocol, CmdType.Error,notify.Feedback);
        }

    }
}

