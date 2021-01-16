using Google.Protobuf.Collections;
using PureMVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UISelectWorkerProxy : Proxy
{
    public UISelectWorkerProxy() : base(null)
    {
        ProxyName = this.GetType().ToString();
    }
    public RepeatedField<UserMessageModel> friendList { get; private set; }
    public RepeatedField<UserMessageModel> selectList { get; private set; }
    public void AddSelect(UserMessageModel user)
    {
        selectList.Add(user);
    }
    public void RemoveSelect(UserMessageModel user)
    {
        selectList.Remove(user);
    }
    public void HandleProject_Check(Notify notify)
    {
        if (notify.Feedback == FeedBack.Success)
        {
            Friend_CheckRsp checkrsp = Friend_CheckRsp.Parser.ParseFrom(notify.message);
            friendList = checkrsp.UsermessageList;
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter, friendList);
        }
        else
        {
            GameFacade.SendNotification(notify.Protocol, CmdType.Error, notify.Feedback);
        }
    }
}

