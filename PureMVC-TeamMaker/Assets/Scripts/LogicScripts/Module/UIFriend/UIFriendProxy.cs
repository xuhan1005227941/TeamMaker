using Google.Protobuf.Collections;
using PureMVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIFriendProxy : Proxy
{
    public UIFriendProxy() : base(null)
    {
        ProxyName = this.GetType().ToString();
    }
    public UserMessageModel chosefriend { get; private set; }
    public RepeatedField<UserMessageModel> friendList { get; private set; }
    public RepeatedField<UserMessageModel> friendApplyList { get; private set; }
    public RepeatedField<UserMessageModel> blackList { get; private set; }
    public override void OnRegister()
    {
    }

    public override void OnRemove()
    {
    }


    public void SetFriendCheck(Notify notify)
    {
        if (notify.Feedback==FeedBack.Success)
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
    public void SetFriendAdd(Notify notify)
    {
        if (notify.Feedback == FeedBack.Success)
        {
            Friend_AddRsp addrsp = Friend_AddRsp.Parser.ParseFrom(notify.message);
            friendList = addrsp.UsermessageList;
            GameFacade.SendNotification(Protocol.Friend_Check, CmdType.Presenter, friendList);
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter);
        }
        else
        {
            GameFacade.SendNotification(notify.Protocol, CmdType.Error, notify.Feedback);
        }
    }
    public void SetFriendDelete(Notify notify)
    {
        if (notify.Feedback == FeedBack.Success)
        {
            Friend_DeleteRsp deletersp = Friend_DeleteRsp.Parser.ParseFrom(notify.message);
            friendList = deletersp.UsermessageList;
            GameFacade.SendNotification(Protocol.Friend_Check, CmdType.Presenter, friendList);
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter);
        }
        else
        {
            GameFacade.SendNotification(notify.Protocol, CmdType.Error, notify.Feedback);
        }
    }

    public void SetLookFirend(Notify notify)
    {
        if (notify.Feedback == FeedBack.Success)
        {
            UserMessage_LookRsp lookRsp = UserMessage_LookRsp.Parser.ParseFrom(notify.message);
            chosefriend = lookRsp.User;
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter, chosefriend);
        }
        else
        {
            GameFacade.SendNotification(notify.Protocol, CmdType.Error, notify.Feedback);
        }
    }

    public void SetFriendApplyCheck(Notify notify)
    {
        if (notify.Feedback == FeedBack.Success)
        {
            FriendApply_CheckRsp checkrsp = FriendApply_CheckRsp.Parser.ParseFrom(notify.message);
            friendApplyList = checkrsp.UsermessageList;
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter, friendApplyList);
        }
        else
        {
            GameFacade.SendNotification(notify.Protocol, CmdType.Error, notify.Feedback);
        }
    }

    public void SetFriendApplyAgree(Notify notify)
    {
        if (notify.Feedback == FeedBack.Success)
        {
            FriendApply_AgreeRsp agreeRsp = FriendApply_AgreeRsp.Parser.ParseFrom(notify.message);
            friendApplyList = agreeRsp.ApplyList;
            friendList = agreeRsp.FriendList;
            GameFacade.SendNotification(Protocol.FriendApply_Check, CmdType.Presenter, friendApplyList);
            GameFacade.SendNotification(Protocol.Friend_Check, CmdType.Presenter, friendList);
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter);
        }
        else
        {
            GameFacade.SendNotification(notify.Protocol, CmdType.Error, notify.Feedback);
        }
    }

    public void SetFriendApplyDisagree(Notify notify)
    {
        if (notify.Feedback == FeedBack.Success)
        {
            FriendApply_DisagreeRsp disaRsp = FriendApply_DisagreeRsp.Parser.ParseFrom(notify.message);
            friendApplyList = disaRsp.ApplyList;
            friendList = disaRsp.FriendList;
            GameFacade.SendNotification(Protocol.FriendApply_Check, CmdType.Presenter, friendApplyList);
            GameFacade.SendNotification(Protocol.Friend_Check, CmdType.Presenter, friendList);
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter, friendApplyList);
        }
        else
        {
            GameFacade.SendNotification(notify.Protocol, CmdType.Error, notify.Feedback);
        }
    }

    public void SetBlackListCheck(Notify notify)
    {
        if (notify.Feedback == FeedBack.Success)
        {
            BlackList_CheckRsp checkrsp = BlackList_CheckRsp.Parser.ParseFrom(notify.message);
            blackList = checkrsp.UsermessageList;
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter, blackList);
        }
        else
        {
            GameFacade.SendNotification(notify.Protocol, CmdType.Error, notify.Feedback);
        }
    }
    public void SetBlackListAdd(Notify notify)
    {
        if (notify.Feedback == FeedBack.Success)
        {
            BlackList_AddRsp addrsp = BlackList_AddRsp.Parser.ParseFrom(notify.message);
            friendApplyList = addrsp.ApplyList;
            friendList = addrsp.FriendList;
            blackList = addrsp.BlackList;
            GameFacade.SendNotification(Protocol.FriendApply_Check, CmdType.Presenter, friendApplyList);
            GameFacade.SendNotification(Protocol.Friend_Check, CmdType.Presenter, friendList);
            GameFacade.SendNotification(Protocol.BlackList_Check, CmdType.Presenter, blackList);
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter);
        }
        else
        {
            GameFacade.SendNotification(notify.Protocol, CmdType.Error, notify.Feedback);
        }
    }
    public void SetBlackListDelete(Notify notify)
    {
        if (notify.Feedback == FeedBack.Success)
        {
            BlackList_DeleteRsp deletersp = BlackList_DeleteRsp.Parser.ParseFrom(notify.message);
            blackList = deletersp.UsermessageList;
            GameFacade.SendNotification(Protocol.BlackList_Check, CmdType.Presenter, blackList);
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter);
        }
        else
        {
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter, notify.Feedback);
        }
    }
}

