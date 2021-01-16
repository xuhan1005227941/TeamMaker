using Google.Protobuf.Collections;
using JetBrains.Annotations;
using PureMVC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFriendMediator : Mediator
{
    public UIFriendMediator() : base(null)
    {
        MediatorName = this.GetType().ToString();
    }

    private string ObjLoadPath = "Prefab/UIFriend";
    private GameObject @object;
    private Button Btn_CloseFriend;
    private Button Btn_FriendSetting;
    private Transform FriendsFather;
    private GameObject Group_FriendSetting;
    private Button Btn_CloseFriendSetting;
    private Button Btn_BlackList;
    private Button Btn_AdditionFriend;
    private Button Btn_ApplyFriend;
    private GameObject Field_BlackList;
    private Transform BlackListFather;
    private GameObject Field_AdditionFriend;
    private InputField Ipt_SelectID;
    private Button Btn_SelectConfirm;
    private Transform AdditionFriendFather;
    private GameObject Field_ApplyFriend;
    private Transform ApplyFriendFather;


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
        obj.transform.name = "UIFriend";
        @object = obj;
        Btn_CloseFriend = @object.transform.FindButton("Btn_CloseFriend", OnClickCloseFriend);
        Btn_FriendSetting = @object.transform.FindButton("Btn_FriendSetting", OnClickFriendSetting);
        FriendsFather = @object.transform.FindeDeepChild("FriendsFather");
        Group_FriendSetting = @object.transform.FindeDeepChild("Group_FriendSetting").gameObject;
        Btn_CloseFriendSetting = @object.transform.FindButton("Btn_CloseFriendSetting", OnClickCloseFriendSetting);
        Btn_BlackList = @object.transform.FindButton("Btn_BlackList", OnClickBlackList);
        Btn_AdditionFriend = @object.transform.FindButton("Btn_AdditionFriend", OnClickAdditionFriend);
        Btn_ApplyFriend = @object.transform.FindButton("Btn_ApplyFriend", OnClickApplyFriend);
        Field_BlackList = @object.transform.FindeDeepChild("Field_BlackList").gameObject;
        BlackListFather = @object.transform.FindeDeepChild("BlackListFather");
        Field_AdditionFriend = @object.transform.FindeDeepChild("Field_AdditionFriend").gameObject;
        Ipt_SelectID = @object.transform.FindeDeepChild<InputField>("Ipt_SelectID");
        Btn_SelectConfirm = @object.transform.FindButton("Btn_SelectConfirm", OnClickSelectConfirm);
        AdditionFriendFather = @object.transform.FindeDeepChild("AdditionFriendFather");
        Field_ApplyFriend = @object.transform.FindeDeepChild("Field_ApplyFriend").gameObject;
        ApplyFriendFather = @object.transform.FindeDeepChild("ApplyFriendFather");

        RequestManager.Execute(Protocol.Friend_Check, GameFacade.RetrieveProxy<PlayerDataProxy>().user.Uid);

    }

    private void OnClickCloseFriend()
    {
        UIManager.PopUI();
    }

    private void OnClickFriendSetting()
    {
        Group_FriendSetting.SetActive(true);
        OnClickApplyFriend();
    }

    private void OnClickCloseFriendSetting()
    {
        Group_FriendSetting.SetActive(false);
    }

    private void OnClickBlackList()
    {
        Field_BlackList.SetActive(true);
        Field_AdditionFriend.SetActive(false);
        Field_ApplyFriend.SetActive(false);
        RequestManager.Execute(Protocol.BlackList_Check, GameFacade.RetrieveProxy<PlayerDataProxy>().user.Uid);
    }

    private void OnClickAdditionFriend()
    {
        Field_BlackList.SetActive(false);
        Field_ApplyFriend.SetActive(false);
        Field_AdditionFriend.SetActive(true);
    }

    public void OnClickApplyFriend()
    {
        Field_BlackList.SetActive(false);
        Field_AdditionFriend.SetActive(false);
        Field_ApplyFriend.SetActive(true);
        RequestManager.Execute(Protocol.FriendApply_Check, GameFacade.RetrieveProxy<PlayerDataProxy>().user.Uid);
    }

    private void OnClickSelectConfirm()
    {
        if (!string.IsNullOrEmpty(Ipt_SelectID.text))
        {
            if (Ipt_SelectID.text.Equals(GameFacade.RetrieveProxy<PlayerDataProxy>().user.Uid))
            {
                GameFacade.ShowTips("自己的ID");
            }
            else
            {
                RequestManager.Execute(Protocol.UserMessage_Look, Ipt_SelectID.text);
            }

        }
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
            friend.transform.Find("Btn_Black").GetComponent<Button>().onClick.AddListener(delegate {
                UIManager.ShowMessageBox("拉黑好友", "确认拉黑?", false, delegate {
                    RequestManager.Execute(Protocol.BlackList_Add, GameFacade.RetrieveProxy<PlayerDataProxy>().user.Uid, userMessage.Uid);
                });
            });
            friend.transform.Find("Btn_Delete").GetComponent<Button>().onClick.AddListener(delegate {
                UIManager.ShowMessageBox("删除好友", "确认删除,一旦删除无法恢复", false, delegate {
                    RequestManager.Execute(Protocol.Friend_Delete,GameFacade.RetrieveProxy<PlayerDataProxy>().user.Uid ,userMessage.Uid);
                });
            });

        }
    }

    public void SetFriendAddition(RepeatedField<UserMessageModel> addition)
    {
        GameObject additionprefab = AdditionFriendFather.GetChild(0).gameObject;
        while (AdditionFriendFather.childCount > 1)
        {
            GameObject.DestroyImmediate(AdditionFriendFather.GetChild(1).gameObject);
        }
        for (int i = 0; i < addition.Count; i++)
        {
            GameObject additionfriend = GameObject.Instantiate(additionprefab, AdditionFriendFather);
            additionfriend.SetActive(true);
            additionfriend.transform.Find("Txt_FriendName").GetComponent<Text>().text = addition[i].UserName;
            additionfriend.transform.Find("Txt_FriendAge").GetComponent<Text>().text = addition[i].UserAge.ToString();
            additionfriend.transform.Find("Txt_FriendProfession").GetComponent<Text>().text = addition[i].UserProfession;
            additionfriend.transform.Find("Txt_FriendScore").GetComponent<Text>().text = addition[i].UserScore.ToString();
            UserMessageModel userMessage = addition[i];
            additionfriend.transform.FindButton("Btn_ShowMsg", delegate {
                GameFacade.RetrieveProxy<UILobbyProxy>().SetChoseFirend(userMessage);
            });
            additionfriend.transform.FindButton("Btn_Addition", delegate {
                RequestManager.Execute(Protocol.Friend_Add, GameFacade.RetrieveProxy<PlayerDataProxy>().user.Uid, userMessage.Uid);
            });
        }
    }



    public void SetFriendApply(RepeatedField<UserMessageModel> apply)
    {
        GameObject applyprefab = ApplyFriendFather.GetChild(0).gameObject;
        while (ApplyFriendFather.childCount > 1)
        {
            GameObject.DestroyImmediate(ApplyFriendFather.GetChild(1).gameObject);
        }

        for (int i = 0; i < apply.Count; i++)
        {
            GameObject applyfriend = GameObject.Instantiate(applyprefab, ApplyFriendFather);
            applyfriend.SetActive(true);
            applyfriend.transform.Find("Txt_FriendName").GetComponent<Text>().text = apply[i].UserName;
            applyfriend.transform.Find("Txt_FriendAge").GetComponent<Text>().text = apply[i].UserAge.ToString();
            applyfriend.transform.Find("Txt_FriendProfession").GetComponent<Text>().text = apply[i].UserProfession;
            applyfriend.transform.Find("Txt_FriendScore").GetComponent<Text>().text = apply[i].UserScore.ToString();
            string fID = apply[i].Uid.ToString();
            UserMessageModel userMessage = apply[i];
            applyfriend.transform.FindButton("Btn_ShowMsg", delegate {
                GameFacade.RetrieveProxy<UILobbyProxy>().SetChoseFirend(userMessage);
            });
            applyfriend.transform.Find("Btn_Agree").GetComponent<Button>().onClick.AddListener(delegate {
                RequestManager.Execute(Protocol.FriendApply_Agree,GameFacade.RetrieveProxy<PlayerDataProxy>().user.Uid, fID);

            });
            applyfriend.transform.Find("Btn_Disagree").GetComponent<Button>().onClick.AddListener(delegate {
                RequestManager.Execute(Protocol.FriendApply_Disagree, GameFacade.RetrieveProxy<PlayerDataProxy>().user.Uid, fID);
            });

        }
    }

    public void SetBlackList(RepeatedField<UserMessageModel> black)
    {
        GameObject blackprefab = BlackListFather.GetChild(0).gameObject;
        while (BlackListFather.childCount > 1)
        {
            GameObject.DestroyImmediate(BlackListFather.GetChild(1).gameObject);
        }

        for (int i = 0; i < black.Count; i++)
        {
            GameObject friend = GameObject.Instantiate(blackprefab, BlackListFather);
            friend.SetActive(true);
            friend.transform.Find("Txt_FriendName").GetComponent<Text>().text = black[i].UserName;
            friend.transform.Find("Txt_FriendAge").GetComponent<Text>().text = black[i].UserAge.ToString();
            friend.transform.Find("Txt_FriendProfession").GetComponent<Text>().text = black[i].UserProfession;
            friend.transform.Find("Txt_FriendScore").GetComponent<Text>().text = black[i].UserScore.ToString();
            string fID = black[i].Uid.ToString();
            UserMessageModel userMessage = black[i];
            friend.transform.FindButton("Btn_ShowMsg", delegate {
                GameFacade.RetrieveProxy<UILobbyProxy>().SetChoseFirend(userMessage);
            });
            friend.transform.Find("Btn_Delete").GetComponent<Button>().onClick.AddListener(delegate {
                RequestManager.Execute(Protocol.BlackList_Delete, GameFacade.RetrieveProxy<PlayerDataProxy>().user.Uid, fID);
            });

        }
    }

    public override string[] ListNotificationInterests()
    {
        string[] interests = new string[] {
            Protocol.Friend_Check.ToString(),
            Protocol.Friend_Add.ToString(),Protocol.Friend_Delete.ToString(),
            Protocol.UserMessage_Look.ToString(),
            Protocol.FriendApply_Check.ToString(),Protocol.FriendApply_Agree.ToString(),
            Protocol.FriendApply_Disagree.ToString(),Protocol.BlackList_Add.ToString(),
            Protocol.BlackList_Check.ToString(),Protocol.BlackList_Delete.ToString()
        };
        return interests;
    }


    public override void HandleNotification(INotification notification)
    {
        int mainproto = int.Parse(notification.Name);
        CmdType type = (CmdType)int.Parse(notification.Type);

            switch (mainproto)
            {
                case Protocol.Friend_Add:
                    HandleFriend_Add(type,notification.Body);
                    break;
                case Protocol.Friend_Check:
                    HandleFriend_Check(type,notification.Body);
                    break;
                case Protocol.Friend_Delete:
                    HandleFriend_Delete(type,notification.Body);
                    break;
                case Protocol.UserMessage_Look:
                    HandleUserMessage_Look(type, notification.Body);
                break;
                case Protocol.FriendApply_Agree:
                HandleFriendApply_Agree(type, notification.Body);
                    break;
                case Protocol.FriendApply_Disagree:
                HandleFriendApply_Disagree(type, notification.Body);
                    break;
                case Protocol.FriendApply_Check:
                    HandleFriendApply_Check(type, notification.Body);
                    break;
                case Protocol.BlackList_Add:
                    HandleBlackList_Add(type, notification.Body);
                    break;
                case Protocol.BlackList_Delete:
                    HandleBlackList_Delete(type, notification.Body);
                    break;
                case Protocol.BlackList_Check:
                    HandleBlackList_Check(type, notification.Body);
                    break;
            }

    }


    void HandleFriend_Add(CmdType type,object obj)
    {
        if (type == CmdType.Presenter)
        {
            GameFacade.ShowTips("添加成功");

        }
        else if (type == CmdType.Error)
        {
            GameFacade.ShowTips(FeedBack.Log((int)obj));
        }
    }
    void HandleFriend_Check(CmdType type, object obj)
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
    void HandleFriend_Delete(CmdType type, object obj)
    {
        if (type == CmdType.Presenter)
        {
            GameFacade.ShowTips("删除成功");

        }
        else if (type == CmdType.Error)
        {
            GameFacade.ShowTips(FeedBack.Log((int)obj));
        }
       
    }

    void HandleUserMessage_Look(CmdType type, object obj)
    {
        if (type == CmdType.Presenter)
        {
            UserMessageModel Rsp = obj as UserMessageModel;
            RepeatedField<UserMessageModel> addions = new RepeatedField<UserMessageModel>();
            addions.Add(Rsp);
            SetFriendAddition(addions);
        }
        else if (type == CmdType.Error)
        {
            GameFacade.ShowTips(FeedBack.Log((int)obj));
        }
    }

    void HandleFriendApply_Agree(CmdType type, object obj)
    {
        if (type == CmdType.Presenter)
        {
            GameFacade.ShowTips("你已同意对方好友申请");

        }
        else if (type == CmdType.Error)
        {
            GameFacade.ShowTips(FeedBack.Log((int)obj));
        }
        
    }
    void HandleFriendApply_Disagree(CmdType type, object obj)
    {
        if (type == CmdType.Presenter)
        {
            GameFacade.ShowTips("你已拒绝对方好友申请");

        }
        else if (type == CmdType.Error)
        {
            GameFacade.ShowTips(FeedBack.Log((int)obj));
        }
        
    }
    void HandleFriendApply_Check(CmdType type, object obj)
    {
        if (type == CmdType.Presenter)
        {
            RepeatedField<UserMessageModel> Rsp = obj as RepeatedField<UserMessageModel>;
            SetFriendApply(Rsp);
        }
        else if (type == CmdType.Error)
        {
            GameFacade.ShowTips(FeedBack.Log((int)obj));
        }
    }

    void HandleBlackList_Add(CmdType type, object obj)
    {
        if (type == CmdType.Presenter)
        {
            GameFacade.ShowTips("将对方添加进黑名单");

        }
        else if (type == CmdType.Error)
        {
            GameFacade.ShowTips(FeedBack.Log((int)obj));
        }
        
    }
    void HandleBlackList_Delete(CmdType type, object obj)
    {
        if (type == CmdType.Presenter)
        {
            GameFacade.ShowTips("将对方从黑名单里移除");

        }
        else if (type == CmdType.Error)
        {
            GameFacade.ShowTips(FeedBack.Log((int)obj));
        }
        
    }
    void HandleBlackList_Check(CmdType type, object obj)
    {
        if (type == CmdType.Presenter)
        {
            RepeatedField<UserMessageModel> Rsp = obj as RepeatedField<UserMessageModel>;
            SetBlackList(Rsp);
        }
        else if (type == CmdType.Error)
        {
            GameFacade.ShowTips(FeedBack.Log((int)obj));
        }
        
    }
}
