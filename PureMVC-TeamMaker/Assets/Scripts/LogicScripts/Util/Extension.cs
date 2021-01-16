using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Threading;
using UnityEngine.Events;

public static class Extension {

    private static bool ButonKey;
    private static Thread ButonThread;
    public static Transform FindeDeepChild(this Transform parent, string child)
    {
        Transform tf = parent.Find(child);
        if (tf != null)
        {
            return tf;
        }
        for (int i = 0; i < parent.childCount; i++)
        {

            Transform target = FindeDeepChild(parent.GetChild(i), child);
            if (target != null)
            {
                return target;
            }
        }
        return null;
    }

    public static T FindeDeepChild<T>(this Transform parent, string child)
    {
        Transform tf = parent.Find(child);
        if (tf != null)
        {
            return tf.GetComponent<T>();
        }
        for (int i = 0; i < parent.childCount; i++)
        {

            Transform target = FindeDeepChild(parent.GetChild(i), child);
            if (target != null)
            {
                return target.GetComponent<T>();
            }
        }
        return default(T);
    }

    public static Button FindButton(this Transform parent, string child, Action hander)
    {
        Transform tf = parent.Find(child);
        if (tf != null)
        {
            tf.GetComponent<Button>().onClick.RemoveAllListeners();
            tf.GetComponent<Button>().onClick.AddListener(delegate { ButtonControl(hander); });
            return tf.GetComponent<Button>();
        }
        for (int i = 0; i < parent.childCount; i++)
        {

            Transform target = FindeDeepChild(parent.GetChild(i), child);
            if (target != null)
            {
                target.GetComponent<Button>().onClick.RemoveAllListeners();
                target.GetComponent<Button>().onClick.AddListener(delegate { ButtonControl(hander); });
                return target.GetComponent<Button>();
            }
        }
        return null;
    }

    public static Dropdown FindDropdown(this Transform parent, string child,UnityAction<int> unityAction)
    {
        Transform tf = parent.Find(child);
        if (tf != null)
        {
            tf.GetComponent<Dropdown>().onValueChanged.RemoveAllListeners();
            tf.GetComponent<Dropdown>().onValueChanged.AddListener(unityAction);
            return tf.GetComponent<Dropdown>();
        }
        for (int i = 0; i < parent.childCount; i++)
        {

            Transform target = FindeDeepChild(parent.GetChild(i), child);
            if (target != null)
            {
                target.GetComponent<Dropdown>().onValueChanged.RemoveAllListeners();
                target.GetComponent<Dropdown>().onValueChanged.AddListener(unityAction);
                return target.GetComponent<Dropdown>();
            }
        }
        return null;
    }


    private static void ButtonControl(Action hander)
    {
        if (ButonKey==false)
        {
            hander();
            ButonKey = true;
            ButonThread = new Thread(KeyLose);
            ButonThread.Start();
        }
    }

    private static void KeyLose()
    {
        Thread.Sleep(300);
        ButonKey = false;
        ButonThread.Abort();
    } 
}
