using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIShowUser : IUI
{
    private string ObjLoadPath = "Prefab/UIShowUser";
    private GameObject @object;
    private GameObject Group_ShowMsg;
    private Text Txt_UserID;
    private Text Txt_UserName;
    private Text Txt_UserAge;
    private Text Txt_UserProfession;
    private Text Txt_UserScore;
    private Button Btn_ExitShowMsg;


    public void OnCreate()
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
        ShowUserMessage(GameFacade.RetrieveProxy<UILobbyProxy>().chosefriend);
    }
    public void OnClose()
    {
        @object.SetActive(false);
    }
    public void OnFrozen(bool isF)
    {
        if (isF)
        {

        }
        else
        {

        }
    }

    private void SetView()
    {
        GameObject obj = GameObject.Instantiate(Resources.Load<GameObject>(ObjLoadPath), UIManager.UIRoot.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.transform.name = "UIShowMsg";
        @object = obj;
        Group_ShowMsg = @object.transform.FindeDeepChild("Group_ShowMsg").gameObject;
        Txt_UserID = @object.transform.FindeDeepChild<Text>("Txt_UserID");
        Txt_UserName = @object.transform.FindeDeepChild<Text>("Txt_UserName");
        Txt_UserAge = @object.transform.FindeDeepChild<Text>("Txt_UserAge");
        Txt_UserProfession = @object.transform.FindeDeepChild<Text>("Txt_UserProfession");
        Txt_UserScore = @object.transform.FindeDeepChild<Text>("Txt_UserScore");
        Btn_ExitShowMsg = @object.transform.FindButton("Btn_ExitShowMsg", OnClickExitShowMsg);
    }


    public void ShowUserMessage(UserMessageModel userMessageModel)
    {
        Txt_UserID.text = userMessageModel.Uid.ToString();
        Txt_UserName.text = userMessageModel.UserName;
        Txt_UserAge.text = userMessageModel.UserAge.ToString();
        Txt_UserProfession.text = userMessageModel.UserProfession;
        Txt_UserScore.text = userMessageModel.UserScore.ToString();
        Group_ShowMsg.SetActive(true);

    }

    void OnClickExitShowMsg()
    {
        UIManager.PopUI();
    }

  


}
