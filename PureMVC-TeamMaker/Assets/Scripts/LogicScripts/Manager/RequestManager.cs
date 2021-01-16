using System.Collections;
using System.Collections.Generic;
using System;
using Google.Protobuf;
using UnityEngine;

public static class RequestManager
{
    public static void Execute(int proto,params object[] msg)
    {
        Message mes = ReferencePool.Require<Message>();
        mes.args = new List<string>();
        Debug.Log("-><color=#00EEEE>" + "发送消息 :" + proto + "</color>");
        for (int i = 0; i < msg.Length; i++)
        {
            mes.args.Add(msg[i].ToString());
            Debug.Log(msg[i].ToString());
        }

        GameFacade.SendNotification(proto, CmdType.Request, mes);
    }

}

