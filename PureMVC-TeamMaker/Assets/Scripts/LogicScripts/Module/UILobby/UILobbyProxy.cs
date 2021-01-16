using System;
using PureMVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf.Collections;

public class UILobbyProxy : Proxy
{
    public UILobbyProxy() : base(null)
    {
        ProxyName = this.GetType().ToString();
    }
    public UserMessageModel chosefriend { get; private set; }
    public RepeatedField<ProjectModel> projectList { get; private set; }
    public void SetChoseFirend(UserMessageModel user)
    {
        chosefriend = user;
        UIManager.PushUI<UIShowUser>();
    }
    public void SetProjectCheck(Notify notify)
    {
        if (notify.Feedback==FeedBack.Success)
        {
            Project_CheckRsp checkRsp = Project_CheckRsp.Parser.ParseFrom(notify.message);
            projectList = checkRsp.Projectlist;
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter, projectList);
        }
        else
        {
            GameFacade.SendNotification(notify.Protocol, CmdType.Error,notify.Feedback);
        }
    }

    public void SetProjectAdd(Notify notify)
    {
        if (notify.Feedback == FeedBack.Success)
        {
            Project_AddRsp addRsp = Project_AddRsp.Parser.ParseFrom(notify.message);
            projectList = addRsp.Projectlist;
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter, projectList);
        }
        else
        {
            GameFacade.SendNotification(notify.Protocol, CmdType.Error, notify.Feedback);
        }
    }

    public void SetProjectDelete(Notify notify)
    {
        if (notify.Feedback == FeedBack.Success)
        {
            Project_DeleteRsp addRsp = Project_DeleteRsp.Parser.ParseFrom(notify.message);
            projectList = addRsp.Projectlist;
            GameFacade.SendNotification(notify.Protocol, CmdType.Presenter, projectList);
        }
        else
        {
            GameFacade.SendNotification(notify.Protocol, CmdType.Error, notify.Feedback);
        }
    }
}

