
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
public class Protocol
{
    
    public const int Login = 100101;//账号登陆
    public const int Regist = 100201;//账号注册
    public const int UserMessage_Update = 100301;//更新个人中心
    public const int UserMessage_Look = 100302;//角色详情

    public const int Friend_Check = 100401;//获取好友列表
    public const int Friend_Add = 100402;//增加好友
    public const int Friend_Delete = 100403;//删除好友
    
    public const int FriendApply_Check = 100501;//获取好友申请列表
    public const int FriendApply_Agree = 100502;//同意申请
    public const int FriendApply_Disagree = 100503;//拒绝申请

    public const int BlackList_Check = 100601;//获取黑名单
    public const int BlackList_Add = 100602;
    public const int BlackList_Delete = 100603;

    public const int Project_Check = 100701;//获取项目清单
    public const int Project_Add = 100702;
    public const int Project_Delete = 100703;
    public const int Project_Update = 100704;

}

public class CMD
{
    public const int GameStart = 1;//游戏开始
    public const int SelectWorker = 2;//选择工具人
}

public class FeedBack
{
    public const int Success = 200;
    public const int Failed = 500;
    public const int NotFind = 404;
    public const int NoAccount = 100;
    public const int NoPassword = 101;
    public const int AccountExist = 102;
    public const int AlreadyFriend = 103;
    public const int NoFriendExist = 104;
    public const int CantAddSelf = 105;
    public const int HeInYourBlackList = 106;
    public const int YouInHisBlackList = 107;
    public const int AlreadyApplyFriend = 108;

    private static Dictionary<int, string> errorDic;
    public static void Init()
    {
        errorDic = new Dictionary<int, string>();
        errorDic.Add(200, "成功");
        errorDic.Add(500, "故障");
        errorDic.Add(404, "空空如也");
        errorDic.Add(100, "无账号");
        errorDic.Add(101, "密码错误");
        errorDic.Add(102, "账号已存在");
        errorDic.Add(103, "该玩家已存在好友列表");
        errorDic.Add(104, "该好友不存在");
        errorDic.Add(105, "不能添加自己");
        errorDic.Add(106, "对方在你的黑名单中");
        errorDic.Add(107, "你在对方的黑名单中");
        errorDic.Add(108, "已经申请过,请耐心等待");

    }
    public static string Log(int code)
    {
        string msg = "错误码异常";
        if (errorDic==null)
        {
            Init();
        }
        foreach (var item in errorDic)
        {
            if (code==item.Key)
            {
                msg=item.Value;
            }
        }
        return msg;
    }
}


public enum CmdType
{
    Normal,//一般
    Request,// 请求
    Response,//回调
    Presenter,//同步
    Error// 错误
}
