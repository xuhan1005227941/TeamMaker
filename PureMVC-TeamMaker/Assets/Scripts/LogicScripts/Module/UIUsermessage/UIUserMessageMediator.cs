using JetBrains.Annotations;
using PureMVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUserMessageMediator : Mediator
{
    public UIUserMessageMediator() : base(null)
    {
        MediatorName = this.GetType().ToString();
    }

    private string ObjLoadPath = "Prefab/UIUsermessage";
    private GameObject @object;
    public Text Txt_UserID;
    public InputField Ipt_UserName;
    public InputField Ipt_UserAge;
    public InputField Ipt_UserProfession;
    public InputField Ipt_UserScore;
    public Button Btn_ConfirmMsg;
    public Button Btn_ExitMsg;

    public override void OnRegister()
    {
        if (@object)
        {
            @object.transform.SetAsLastSibling();
            @object.SetActive(true);
        }
        else
        {
            SetView();
        }
    }
    public override void OnRemove()
    {
        @object.SetActive(false);
    }

    private void SetView()
    {
        GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(ObjLoadPath), UIManager.UIRoot.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.name = "UIUsermessage";
        @object = obj;
        Txt_UserID = @object.transform.FindeDeepChild<Text>("Txt_UserID");
        Ipt_UserName = @object.transform.FindeDeepChild<InputField>("Ipt_UserName");
        Ipt_UserAge = @object.transform.FindeDeepChild<InputField>("Ipt_UserAge");
        Ipt_UserProfession = @object.transform.FindeDeepChild<InputField>("Ipt_UserProfession");
        Ipt_UserScore = @object.transform.FindeDeepChild<InputField>("Ipt_UserScore");
        Btn_ConfirmMsg = @object.transform.FindButton("Btn_ConfirmMsg", OnClickConfirmMsg);
        Btn_ExitMsg = @object.transform.FindButton("Btn_ExitMsg", OnClickExitMsg);
        UserMessageModel my = GameFacade.RetrieveProxy<PlayerDataProxy>().user;
        SetPlayer(my);
    }

    void SetPlayer(UserMessageModel my)
    {
        Txt_UserID.text = my.Uid.ToString();
        Ipt_UserName.text = my.UserName;
        Ipt_UserAge.text = my.UserAge.ToString();
        Ipt_UserProfession.text = my.UserProfession;
        Ipt_UserScore.text = my.UserScore.ToString();
    }

    private void OnClickConfirmMsg()
    {
        RequestManager.Execute(Protocol.UserMessage_Update,
        Txt_UserID.text, Ipt_UserName.text, Ipt_UserAge.text,
        Ipt_UserProfession.text, Ipt_UserScore.text);
    }

    private void OnClickExitMsg()
    {
        UIManager.PopUI();
    }

    public override string[] ListNotificationInterests()
    {
        string[] interests = new string[] {
            Protocol.UserMessage_Update.ToString()
        };
        return interests;
    }

    public override void HandleNotification(INotification notification)
    {
        int mainproto = int.Parse(notification.Name);
        CmdType type = (CmdType)int.Parse(notification.Type);

            switch (mainproto)
            {
                case Protocol.UserMessage_Update:
                    HandleUser_Update(type,notification.Body);
                    break;
            }
    }

    void HandleUser_Update(CmdType type,object msg)
    {
        if (type==CmdType.Presenter)
        {
            GameFacade.ShowTips("修改成功");
            UserMessageModel user = msg as UserMessageModel;
            SetPlayer(user);
        }
        else if (type == CmdType.Error)
        {
            GameFacade.ShowTips(FeedBack.Log((int)msg));
        }
       
    }
}

