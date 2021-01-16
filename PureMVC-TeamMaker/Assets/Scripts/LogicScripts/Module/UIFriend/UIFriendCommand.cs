using Google.Protobuf;
using PureMVC;
using PureMVC.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIFriendCommand:MacroCommand
{
    protected override void InitializeMacroCommand()
    {
        AddSubCommand(delegate { return new UIFriendRequestCommand();});
        AddSubCommand(delegate { return new UIFriendResponseCommand(); });
    }

}

/// <summary>
///  mediator发起请求
/// </summary>
public class UIFriendRequestCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        int mainproto = int.Parse(notification.Name);
        CmdType type =  (CmdType)int.Parse(notification.Type);
        Message message = notification.Body as Message;
        Notify notify = new Notify();
        notify.Protocol = mainproto;
        if (type ==CmdType.Request)
        {
            switch (mainproto)
            {
                case Protocol.Friend_Check:
                    Friend_CheckReq checkReq = ReferencePool.Require<Friend_CheckReq>();
                    checkReq.Uid = int.Parse(message.args[0]);
                    notify.message = checkReq.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;
                case Protocol.Friend_Add:
                    Friend_AddReq addReq = ReferencePool.Require<Friend_AddReq>();
                    addReq.ThisUid = int.Parse(message.args[0]);
                    addReq.ThatUid = int.Parse(message.args[1]);
                    notify.message = addReq.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;
                case Protocol.Friend_Delete:
                    Friend_DeleteReq deleteReq = ReferencePool.Require<Friend_DeleteReq>();
                    deleteReq.ThisUid = int.Parse(message.args[0]);
                    deleteReq.ThatUid = int.Parse(message.args[1]);
                    notify.message = deleteReq.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;
                case Protocol.UserMessage_Look:
                    UserMessage_LookReq lookreq = ReferencePool.Require<UserMessage_LookReq>();
                    lookreq.Uid = int.Parse(message.args[0]);
                    notify.message = lookreq.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;
                case Protocol.FriendApply_Check:
                    FriendApply_CheckReq applycheckReq = ReferencePool.Require<FriendApply_CheckReq>();
                    applycheckReq.Uid = int.Parse(message.args[0]);
                    notify.message = applycheckReq.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;
                case Protocol.FriendApply_Agree:
                    FriendApply_AgreeReq agreeReq = ReferencePool.Require<FriendApply_AgreeReq>();
                    agreeReq.ThisUid = int.Parse(message.args[0]);
                    agreeReq.ThatUid = int.Parse(message.args[1]);
                    notify.message = agreeReq.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;
                case Protocol.FriendApply_Disagree:
                    FriendApply_DisagreeReq disaReq = ReferencePool.Require<FriendApply_DisagreeReq>();
                    disaReq.ThisUid = int.Parse(message.args[0]);
                    disaReq.ThatUid = int.Parse(message.args[1]);
                    notify.message = disaReq.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;
                case Protocol.BlackList_Check:
                    BlackList_CheckReq blackcheck = ReferencePool.Require<BlackList_CheckReq>();
                    blackcheck.Uid = int.Parse(message.args[0]);
                    notify.message = blackcheck.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;
                case Protocol.BlackList_Add:
                    BlackList_AddReq blackaddReq = ReferencePool.Require<BlackList_AddReq>();
                    blackaddReq.ThisUid = int.Parse(message.args[0]);
                    blackaddReq.ThatUid = int.Parse(message.args[1]);
                    notify.message = blackaddReq.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;
                case Protocol.BlackList_Delete:
                    BlackList_DeleteReq blackdeleteReq = ReferencePool.Require<BlackList_DeleteReq>();
                    blackdeleteReq.ThisUid = int.Parse(message.args[0]);
                    blackdeleteReq.ThatUid = int.Parse(message.args[1]);
                    notify.message = blackdeleteReq.ToByteArray();
                    NetworkManager.SendRequest(notify);
                    break;
            }
        }
    }
}

public class UIFriendResponseCommand : SimpleCommand
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
                case Protocol.Friend_Check:
                    GameFacade.RetrieveProxy<UIFriendProxy>().SetFriendCheck(notify);
                    break;
                case Protocol.Friend_Add:
                    GameFacade.RetrieveProxy<UIFriendProxy>().SetFriendAdd(notify);
                    break;
                case Protocol.Friend_Delete:
                    GameFacade.RetrieveProxy<UIFriendProxy>().SetFriendDelete(notify);
                    break;
                case Protocol.UserMessage_Look:
                    GameFacade.RetrieveProxy<UIFriendProxy>().SetLookFirend(notify);
                    break;
                case Protocol.FriendApply_Check:
                    GameFacade.RetrieveProxy<UIFriendProxy>().SetFriendApplyCheck(notify);
                    break;
                case Protocol.FriendApply_Agree:
                    GameFacade.RetrieveProxy<UIFriendProxy>().SetFriendApplyAgree(notify);
                    break;
                case Protocol.FriendApply_Disagree:
                    GameFacade.RetrieveProxy<UIFriendProxy>().SetFriendApplyDisagree(notify);
                    break;
                case Protocol.BlackList_Check:
                    GameFacade.RetrieveProxy<UIFriendProxy>().SetBlackListCheck(notify);
                    break;
                case Protocol.BlackList_Add:
                    GameFacade.RetrieveProxy<UIFriendProxy>().SetBlackListAdd(notify);
                    break;
                case Protocol.BlackList_Delete:
                    GameFacade.RetrieveProxy<UIFriendProxy>().SetBlackListDelete(notify);
                    break;
            }
        }
    }


}
