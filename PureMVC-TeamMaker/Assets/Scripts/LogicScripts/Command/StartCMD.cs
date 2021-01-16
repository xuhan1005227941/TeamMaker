using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC;
public class StartCmd : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        Debug.Log("游戏开始");
        UIManager.PushUI<UILogin>();
        GameObject.Find("GameStart").AddComponent<NetworkManager>();
    }

}
