using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class UITips : IUI
{
    public string Name { get; set; }
    private string ObjLoadPath = "Prefab/UITips";
    private GameObject @object;
    private Text Txt_Tips;

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
        obj.transform.name = "UITips";
        @object = obj;
        Txt_Tips = @object.transform.FindeDeepChild<Text>("Txt_Tips");
    }
    public void ShowTips(string msg)
    {
        OnCreate();
        Txt_Tips.text = msg;
        @object.transform.localPosition = Vector3.zero;
        @object.transform.DOKill();
        @object.transform.DOLocalMoveY(130, 1).OnComplete(OnClose);
    }



    
}
