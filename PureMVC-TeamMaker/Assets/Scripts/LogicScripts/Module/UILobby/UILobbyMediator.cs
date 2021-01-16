using Google.Protobuf.Collections;
using PureMVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILobbyMediator:Mediator
   {
    public UILobbyMediator() : base(null)
    {
        MediatorName = this.GetType().ToString();
    }
    private string ObjLoadPath = "Prefab/UILobby";
    private GameObject @object;
    private Button Btn_UserMessage;
    private Button Btn_BackToLogin;
    private Button Btn_NewProject;
    private Button Btn_AllProject;
    private Button Btn_MyProject;
    private Button Btn_ExileProject;
    private Button Btn_FriendList;
    private Button Btn_Setting;
    public Transform ProjectFather;
    public GameObject ProjectContent;

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
        RequestManager.Execute(Protocol.Project_Check);
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
        obj.transform.name = "UILobby";
        @object = obj;
        Btn_UserMessage = @object.transform.FindButton("Btn_UserMessage", OnClickUserMessage);
        Btn_BackToLogin = @object.transform.FindButton("Btn_BackToLogin", OnClickBackToLogin);
        Btn_NewProject = @object.transform.FindButton("Btn_NewProject", OnClickNewProject);
        Btn_AllProject = @object.transform.FindButton("Btn_AllProject", OnClickAllProject);
        Btn_MyProject = @object.transform.FindButton("Btn_MyProject", OnClickMyProject);
        Btn_ExileProject = @object.transform.FindButton("Btn_ExileProject", OnClickExileProject);
        Btn_FriendList = @object.transform.FindButton("Btn_FriendList", OnClickFriendList);
        Btn_Setting = @object.transform.FindButton("Btn_Setting", OnClickOpenSetting);
        ProjectFather = @object.transform.FindeDeepChild("ProjectFather");
        ProjectContent = @object.transform.FindeDeepChild("ProjectContent").gameObject;
    }

    private void OnClickUserMessage()
    {
        UIManager.PushUI<UIUsermessage>();
    }

    private void OnClickBackToLogin()
    {
        UIManager.SwitchUI<UILogin>();
    }

    private void OnClickNewProject()
    {
        UIManager.PushUI<UINewProject>();
    }

    private void OnClickAllProject()
    {

    }

    private void OnClickMyProject()
    {

    }

    private void OnClickExileProject()
    {

    }

    private void OnClickFriendList()
    {
        UIManager.PushUI<UIFriend>();
    }

    private void OnClickOpenSetting()
    {
    }

    private string SetPriority(int priority)
    {
        string answer = "";
        switch (priority)
        {
            case 0:
                answer= "low";
                break;
            case 1:
                answer = "high";
                break;
            default:
                answer = "high";
                break;
        }
        return answer;
    }

    private void SetProject(RepeatedField<ProjectModel> ProjectList)
    {
        while (ProjectFather.childCount > 1)
        {
            GameObject.DestroyImmediate(ProjectFather.GetChild(1).gameObject);
        }
        for (int i = 0; i < ProjectList.Count; i++)
        {
            GameObject project = GameObject.Instantiate(ProjectContent, ProjectFather);
            project.SetActive(true);
            project.transform.Find("Txt_Index").GetComponent<Text>().text = (i + 1).ToString();
            project.transform.Find("Txt_Name").GetComponent<Text>().text = ProjectList[i].ProjectName;
            project.transform.Find("Txt_Priority").GetComponent<Text>().text = SetPriority(ProjectList[i].Priority);
            project.transform.Find("Txt_Description").GetComponent<Text>().text = ProjectList[i].Description;
            project.transform.Find("Txt_CreateDate").GetComponent<Text>().text = ProjectList[i].CreateDate;
            project.transform.Find("Txt_Completeness").GetComponent<Text>().text = ProjectList[i].Completeness.ToString();
        }
    }

    public override string[] ListNotificationInterests()
    {
        string[] interests = new string[] {
            Protocol.Project_Check.ToString(),
            Protocol.Project_Add.ToString(),
            Protocol.Project_Delete.ToString()
        };
        return interests;
    }

    public override void HandleNotification(INotification notification)
    {
        int mainproto = int.Parse(notification.Name);
        CmdType type = (CmdType)int.Parse(notification.Type);

            switch (mainproto)
            {
                case Protocol.Project_Add:
                HandleProject_Add(type, notification.Body);
                    break;
                case Protocol.Project_Check:
                HandleProject_Check(type, notification.Body);
                break;
                case Protocol.Project_Delete:
                HandleProject_Delete(type, notification.Body);
                break;

            }

    }
    void HandleProject_Check(CmdType type, object obj)
    {
        if (type == CmdType.Presenter)
        {
            RepeatedField<ProjectModel> projectListModel = obj as RepeatedField<ProjectModel>;
            SetProject(projectListModel);
        }
        else if (type == CmdType.Error)
        {
            GameFacade.ShowTips(FeedBack.Log((int)obj));
        }
    }

    void HandleProject_Add(CmdType type, object obj)
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
    void HandleProject_Delete(CmdType type, object obj)
    {
        if (type == CmdType.Presenter)
        {
            GameFacade.ShowTips("删除项目成功");
        }
        else if (type == CmdType.Error)
        {
            GameFacade.ShowTips(FeedBack.Log((int)obj));
        }
    }

}

