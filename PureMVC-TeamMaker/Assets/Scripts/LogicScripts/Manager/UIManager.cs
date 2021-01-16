using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : IManager
{
    public string Name { get ; set; }

    private static Stack<IUI> mUIStack=new Stack<IUI>();

    private static List<IUI> mUICache=new List<IUI>();

    private static Transform mUIRoot;
    public static Transform UIRoot { get {
            if (mUIRoot==null)
            {
                mUIRoot = GameObject.Find("UIRoot").transform;
            }
            return mUIRoot;
        }
    }

    public static IUI FindUI<T>() where T : class,IUI, new()
    {
        IUI uI = mUICache.Find((match) => { return match is T; });
        if (uI == null)
        {   
            uI = ReferencePool.Require<T>() as IUI;
            mUICache.Add(uI);
        }
        return uI;
    }
    public static void SwitchUI<T>() where T : class, IUI, new()
    {
        PopUI();
        PushUI<T>();
    }

    public static void PushUI<T>() where T : class, IUI, new()
    {
        IUI uI = FindUI<T>();
        uI.OnCreate();
        if (mUIStack.Count>0)
        {
        mUIStack.Peek().OnFrozen(true);
        }
        mUIStack.Push(uI);
    }

    public static void PopUI()
    {
        IUI uI = mUIStack.Pop();
        if (mUIStack.Count > 0)
        {
            mUIStack.Peek().OnFrozen(false);
        }
        uI.OnClose();
    }


    public static void ShowTips(string msg)
    {
        UITips uI = FindUI<UITips>() as UITips;
        uI.ShowTips(msg);
    }

    public static void ShowMessageBox(string title,string msg,bool oneBtn,Action action)
    {
        UIMessageBox uIMessageBox = new UIMessageBox();
        uIMessageBox.ShowMessageBox(title, msg, oneBtn, action);
    }

   
}
