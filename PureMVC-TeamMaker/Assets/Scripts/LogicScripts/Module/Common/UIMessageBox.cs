using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

public class UIMessageBox : IUI
{
    public string Name { get; set; }
    private string ObjLoadPath = "Prefab/UIMessageBox";
    private GameObject @object;

    private Text Txt_Title;
    private Text Txt_Message;
    private Button Btn_Confirm;
    private Button Btn_Cancel;
    private Action handler;
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
        obj.transform.name = "UIMessageBox";
        @object = obj;
        Txt_Title = @object.transform.FindeDeepChild<Text>("Txt_Title");
        Txt_Message = @object.transform.FindeDeepChild<Text>("Txt_Message");
        Btn_Confirm = @object.transform.FindButton("Btn_Confirm", OnClickConfirm);
        Btn_Cancel = @object.transform.FindButton("Btn_Cancel", OnClickCancel);
    }

    public void ShowMessageBox(string title, string msg, bool oneBtn,Action action)
    {
        OnCreate();
        Txt_Title.text = title;
        Txt_Message.text = msg;
        Btn_Cancel.gameObject.SetActive(!oneBtn);
        handler = action;
    }

    private void OnClickConfirm()
    {
        this?.handler();
        OnClose();
    }

    private void OnClickCancel()
    {
        OnClose();
    }


 
}
