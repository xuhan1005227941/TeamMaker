

using UnityEngine;

public static class ResponseManager
    {
    public static void Execute(Notify notify)
    {
        Debug.Log("-><color=#FFFF00>" + "收到消息 :" + notify.Protocol + "</color>");
        GameFacade.SendNotification(notify.Protocol, CmdType.Response, notify);
    }


}

