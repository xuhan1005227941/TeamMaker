using Google.Protobuf.Collections;
using JetBrains.Annotations;
using PureMVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UISelectWorkerMediator: Mediator
{
    public UISelectWorkerMediator() : base(null)
    {
    MediatorName = this.GetType().ToString();
    }
    public string Name { get; set; }
    private string ObjLoadPath = "Prefab/UISelectWorker";
    private GameObject @object;
    private Button Btn_CloseSelectFriend;
    private Transform FriendsFather;
    private Button Btn_ConfirmSelectFriend;

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
        RequestManager.Execute(Protocol.Friend_Check,GameFacade.RetrieveProxy<PlayerDataProxy>().user.Uid);
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
        obj.transform.name = "UISelectFriend";
        @object = obj;
        Btn_ConfirmSelectFriend = @object.transform.FindButton("Btn_ConfirmSelectFriend", OnClickConfirmSelectFriend);
        Btn_CloseSelectFriend = @object.transform.FindButton("Btn_CloseSelectFriend", OnClickCloseSelectFriend);
        FriendsFather = @object.transform.FindeDeepChild("FriendsFather");
    }

    private void OnClickConfirmSelectFriend()
    {
        GameFacade.SendNotification(CMD.SelectWorker, GameFacade.RetrieveProxy<UISelectWorkerProxy>().selectList);
        UIManager.PopUI();
    }

    private void OnClickCloseSelectFriend()
    {
        UIManager.PopUI();
    }

    public void SetFriendList(RepeatedField<UserMessageModel> friends)
    {
        GameObject friendprefab = FriendsFather.GetChild(0).gameObject;
        while (FriendsFather.childCount > 1)
        {
            GameObject.DestroyImmediate(FriendsFather.GetChild(1).gameObject);
        }

        for (int i = 0; i < friends.Count; i++)
        {
            GameObject friend = GameObject.Instantiate(friendprefab, FriendsFather);
            friend.SetActive(true);
            friend.transform.Find("Txt_FriendName").GetComponent<Text>().text = friends[i].UserName;
            friend.transform.Find("Txt_FriendAge").GetComponent<Text>().text = friends[i].UserAge.ToString();
            friend.transform.Find("Txt_FriendProfession").GetComponent<Text>().text = friends[i].UserProfession;
            friend.transform.Find("Txt_FriendScore").GetComponent<Text>().text = friends[i].UserScore.ToString();
            UserMessageModel userMessage = friends[i];
            friend.transform.Find("Btn_ShowMsg").GetComponent<Button>().onClick.AddListener(delegate {
                GameFacade.RetrieveProxy<UILobbyProxy>().SetChoseFirend(userMessage);
            });
            friend.transform.FindeDeepChild("Tog_Select").GetComponent<Toggle>().onValueChanged.AddListener(ison => {
                if (ison)
                {
                    GameFacade.RetrieveProxy<UISelectWorkerProxy>().AddSelect(userMessage);
                }
                else
                {
                    GameFacade.RetrieveProxy<UISelectWorkerProxy>().RemoveSelect(userMessage);
                }
            });


        }
    }


    public override string[] ListNotificationInterests()
    {
        string[] interests = new string[] {
            Protocol.Friend_Check.ToString()
        };
        return interests;
    }

    public override void HandleNotification(INotification notification)
    {
        int mainproto = int.Parse(notification.Name);
        CmdType type = (CmdType)Enum.Parse(typeof(CmdType), notification.Type);
        switch (mainproto)
        {
            case Protocol.Friend_Check:
                HandleFriend_Check(type, notification.Body);
                break;

        }
    }

    private void HandleFriend_Check(CmdType type, object obj)
    {
        if (type == CmdType.Presenter)
        {
            RepeatedField<UserMessageModel> Rsp = obj as RepeatedField<UserMessageModel>;
            SetFriendList(Rsp);
        }
        else if (type == CmdType.Error)
        {
            GameFacade.ShowTips(FeedBack.Log((int)obj));
        }
    }
}

