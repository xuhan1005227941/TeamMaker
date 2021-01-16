using JetBrains.Annotations;
using PureMVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILoginMediator : Mediator
{
    public UILoginMediator() : base(null)
    {
        MediatorName = this.GetType().ToString();
    }

    public string Name { get; set; }
    private string ObjLoadPath = "Prefab/UILogin";
    private GameObject @object;
    private InputField Ipt_Account;
    private InputField Ipt_Password;
    private Button Btn_LoginConfirm;
    private Button Btn_RegisterConfirm;
    private Button Btn_Exit;

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

        GameObject obj = Tools.Instantiate<GameObject>(ObjLoadPath, UIManager.UIRoot);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.name = "UILogin";
        @object = obj;
        Ipt_Account = @object.transform.FindeDeepChild<InputField>("Ipt_Account");
        Ipt_Password = @object.transform.FindeDeepChild<InputField>("Ipt_Password");
        Btn_LoginConfirm = @object.transform.FindButton("Btn_LoginConfirm", OnClickLoginConfirm);
        Btn_RegisterConfirm = @object.transform.FindButton("Btn_RegisterConfirm", OnClickRegisterConfirm);
        Btn_Exit = @object.transform.FindButton("Btn_Exit", OnClickExit);
    }

    private void OnClickLoginConfirm()
    {
        if (string.IsNullOrEmpty(Ipt_Account.text) || string.IsNullOrEmpty(Ipt_Password.text))
        {
            GameFacade.ShowTips("账号或者密码不能为空");
            return;
        }
        RequestManager.Execute(Protocol.Login, Ipt_Account.text, Ipt_Password.text);

    }

    private void OnClickRegisterConfirm()
    {
        if (string.IsNullOrEmpty(Ipt_Account.text) || string.IsNullOrEmpty(Ipt_Password.text))
        {
            GameFacade.ShowTips("账号或者密码不能为空");
            return;
        }
        RequestManager.Execute(Protocol.Regist, Ipt_Account.text, Ipt_Password.text);
    }


    public void OnClickExit()
    {
        Application.Quit();
    }
    public override string[] ListNotificationInterests()
    {
        string[] interests = new string[] {
            Protocol.Login.ToString(),
            Protocol.Regist.ToString()
        };
        return interests;
    }

    public override void HandleNotification(INotification notification)
    {
        int mainproto = int.Parse(notification.Name);
        CmdType type = (CmdType)int.Parse(notification.Type);
        switch (mainproto)
        {
            case Protocol.Login:
                HandleLogin(type, notification.Body);
                break;
            case Protocol.Regist:
                HandleRegist(type, notification.Body);
                break;
        }
    }

    void HandleLogin(CmdType type,object msg)
    {
        if (type == CmdType.Presenter)
        {
            GameFacade.ShowTips("login success");
            UIManager.SwitchUI<UILobby>();
        }
        else if (type == CmdType.Error)
        {
            GameFacade.ShowTips(FeedBack.Log((int)msg));
        }
           
    }

    void HandleRegist(CmdType type, object msg)
    {
        if (type == CmdType.Presenter)
        {
            GameFacade.ShowTips("register success");
        }
        else if (type == CmdType.Error)
        {
            GameFacade.ShowTips(FeedBack.Log((int)msg));
        }
        
    }

}
