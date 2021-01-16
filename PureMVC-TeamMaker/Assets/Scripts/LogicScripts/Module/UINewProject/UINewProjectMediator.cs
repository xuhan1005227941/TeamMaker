using Google.Protobuf.Collections;
using JetBrains.Annotations;
using PureMVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


 public class UINewProjectMediator : Mediator
{
    public UINewProjectMediator() : base(null)
    {
        MediatorName = this.GetType().ToString();
    }
    public string Name { get; set; }
    private string ObjLoadPath = "Prefab/UINewProject";
    private GameObject @object;
    private Button Btn_CloseNewproject;
    private GameObject ProjectField;
    private GameObject WorkerField;
    private Button Btn_Next;
    private Button Btn_Last;
    private InputField Ipt_ProjectName;
    private InputField Ipt_Description;
    private Dropdown Dp_Priority;
    private Dropdown Dp_Limit;

    private Transform WorkersFather;
    private Button Btn_AddWorker;
    private Button Btn_DeleteWorker;
    private Button Btn_DeleteAll;
    private Button Btn_SortWorkers;
    private Button Btn_SortWorkersReverse;

    private int m_step = 1;

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
        obj.transform.name = "UINewProject";
        @object = obj;
        Btn_CloseNewproject = @object.transform.FindButton("Btn_CloseNewproject", OnClickCloseNewproject);
        ProjectField = @object.transform.FindeDeepChild("ProjectField").gameObject;
        WorkerField = @object.transform.FindeDeepChild("WorkerField").gameObject;
        Btn_Next = @object.transform.FindButton("Btn_Next", OnClickNext);
        Btn_Last = @object.transform.FindButton("Btn_Last", OnClickLast);
        Ipt_ProjectName = @object.transform.FindeDeepChild<InputField>("Ipt_ProjectName");
        Ipt_Description = @object.transform.FindeDeepChild<InputField>("Ipt_Description");
        Dp_Priority = @object.transform.FindeDeepChild<Dropdown>("Dp_Priority");
        Dp_Limit = @object.transform.FindeDeepChild<Dropdown>("Dp_Limit");
        WorkersFather = @object.transform.FindeDeepChild("WorkersFather");
        Btn_AddWorker = @object.transform.FindButton("Btn_AddWorker", OnClickAddWorker);
        Btn_DeleteWorker = @object.transform.FindButton("Btn_DeleteWorker", OnClickDeleteWorker);
        Btn_DeleteAll = @object.transform.FindButton("Btn_DeleteAll", OnClickDeleteAll);
        Btn_SortWorkers = @object.transform.FindButton("Btn_SortWorkers", OnClickSortWorkers);
        Btn_SortWorkersReverse = @object.transform.FindButton("Btn_SortWorkersReverse", OnClickSortWorkersReverse);

        Dp_Priority.ClearOptions();
        List<Dropdown.OptionData> dropdownList = new List<Dropdown.OptionData>();
        dropdownList.Add(new Dropdown.OptionData("低"));
        dropdownList.Add(new Dropdown.OptionData("高"));
        Dp_Priority.AddOptions(dropdownList);
        Dp_Limit.ClearOptions();
        List<Dropdown.OptionData> dropdownList2 = new List<Dropdown.OptionData>();
        dropdownList2.Add(new Dropdown.OptionData("一天"));
        dropdownList2.Add(new Dropdown.OptionData("三天"));
        dropdownList2.Add(new Dropdown.OptionData("一周"));
        Dp_Limit.AddOptions(dropdownList2);

    }
    bool CheckStepComplete()
    {
        if (m_step == 1)
        {
            if (Ipt_ProjectName.text == null ||
                Ipt_ProjectName.text == "" ||
                Ipt_Description.text == null ||
                Ipt_Description.text == "")
            {
                UIManager.ShowTips("资料不完整");
                return false;
            }
            return true;
        }
        else if (m_step == 2)
        {
            return true;
        }
        return true;
    }

    void StepGo()
    {

        if (m_step == 1)
        {
            m_step = 2;
            ProjectField.gameObject.SetActive(false);
            WorkerField.gameObject.SetActive(true);
        }
        else if (m_step == 2)
        {
            m_step = 1;
            int limitday = 0;
            switch (Dp_Limit.value)
            {
                case 0:
                    limitday = 1;
                    break;
                case 1:
                    limitday = 3;
                    break;
                case 2:
                    limitday = 7;
                    break;
            }
            RequestManager.Execute(Protocol.Project_Add,
               Ipt_ProjectName.text, Dp_Priority.value, Ipt_Description.text,
               DateTime.Now, DateTime.Now.AddDays(limitday));
        }
    }

    private void OnClickNext()
    {
        if (CheckStepComplete())
        {
            StepGo();
        }
    }

    private void OnClickLast()
    {
        m_step = 1;
        ProjectField.gameObject.SetActive(true);
        WorkerField.gameObject.SetActive(false);
    }

    private void OnClickCloseNewproject()
    {
        UIManager.PopUI();
    }

    private void OnClickAddWorker()
    {
        UIManager.PushUI<UISelectWorker>();
    }


    private void OnClickDeleteWorker()
    {

    }

    private void OnClickDeleteAll()
    {

    }

    private void OnClickSortWorkers()
    {

    }

    private void OnClickSortWorkersReverse()
    {

    }

    private void OnClickConfirmNewproject()
    {

    }

    public void SetFriendList(RepeatedField<UserMessageModel> friends)
    {
        GameObject friendprefab = WorkersFather.GetChild(0).gameObject;
        while (WorkersFather.childCount > 1)
        {
            GameObject.DestroyImmediate(WorkersFather.GetChild(1).gameObject);
        }

        for (int i = 0; i < friends.Count; i++)
        {
            GameObject friend = GameObject.Instantiate(friendprefab, WorkersFather);
            friend.SetActive(true);
            friend.transform.Find("Txt_FriendName").GetComponent<Text>().text = friends[i].UserName;
            friend.transform.Find("Txt_FriendAge").GetComponent<Text>().text = friends[i].UserAge.ToString();
            friend.transform.Find("Txt_FriendProfession").GetComponent<Text>().text = friends[i].UserProfession;
            friend.transform.Find("Txt_FriendScore").GetComponent<Text>().text = friends[i].UserScore.ToString();
            UserMessageModel userMessage = friends[i];
            friend.transform.Find("Btn_ShowMsg").GetComponent<Button>().onClick.AddListener(delegate {
                GameFacade.RetrieveProxy<UILobbyProxy>().SetChoseFirend(userMessage);
            });
        }
    }

    public override string[] ListNotificationInterests()
    {
        string[] interests = new string[] {
            Protocol.Project_Add.ToString(),
            CMD.SelectWorker.ToString()
        };
        return interests;
    }



    public override void HandleNotification(INotification notification)
    {

        int mainproto = int.Parse(notification.Name);
        CmdType type = (CmdType)Enum.Parse(typeof(CmdType), notification.Type);
        switch (mainproto)
        {
            case Protocol.Project_Add:
                HandleProject_Add(type, notification.Body);
                break;
            case CMD.SelectWorker:
                HandleSelectWorker(notification.Body);
                break;
        }
    }

    private void HandleSelectWorker(object body)
    {
        RepeatedField<UserMessageModel> selectList = body as RepeatedField<UserMessageModel>;
    }

    private void HandleProject_Add(CmdType type, object obj)
    {
        if (type == CmdType.Presenter)
        {
            GameFacade.ShowTips("添加项目成功");
        }
        else if (type == CmdType.Error)
        {
            GameFacade.ShowTips(FeedBack.Log((int)obj));
        }
    }
   
}

