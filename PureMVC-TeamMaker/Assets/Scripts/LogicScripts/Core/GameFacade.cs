using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC;
using System;
using System.Reflection;
/// <summary>
/// 总外观控制器
/// </summary>
public static class GameFacade
{
    static Facade facade;
    
    /// <summary>
    /// 游戏开始
    /// </summary>
    public static void Install()
    {
        facade = new Facade();
        
    }

    public static void ShowTips(string msg)
    {
        UIManager.ShowTips(msg);
    }

    /// <summary>
    /// 注册命令到controller层
    /// </summary>
    /// <param name="notifiname">命令名字</param>
    public static void RegisterCommand<T>(params string[] notifiname) where T : class, ICommand, new()
    {
        IController controller = Controller.GetInstance(delegate { return new Controller(); });
        for (int i = 0; i < notifiname.Length; i++)
        {
            controller.RegisterCommand(notifiname[i], delegate { return ReferencePool.Require<T>() as ICommand; });
        }
    }

    /// <summary>
    /// 移除命令
    /// </summary>
    /// <param name="notifiname">命令名字</param>
    public static void RemoveCommand(params string[] notifiname)
    {
        IController controller = Controller.GetInstance(delegate { return new Controller(); });
        for (int i = 0; i < notifiname.Length; i++)
        {
            controller.RemoveCommand(notifiname[i]);
        }
    }


    /// <summary>
    /// 注册命令到controller层
    /// </summary>
    /// <param name="notifiname">命令名字</param>
    public static void RegisterCommand<T>(params int[] notifiname) where T : class, ICommand, new()
    {
        IController controller = Controller.GetInstance(delegate { return new Controller(); });
        for (int i = 0; i < notifiname.Length; i++)
        {
            controller.RegisterCommand(notifiname[i].ToString(), delegate { return ReferencePool.Require<T>() as ICommand; });
        }
    }

    /// <summary>
    /// 移除命令
    /// </summary>
    /// <param name="notifiname">命令名字</param>
    public static void RemoveCommand(params int[] notifiname)
    {
        IController controller = Controller.GetInstance(delegate { return new Controller(); });
        for (int i = 0; i < notifiname.Length; i++)
        {
            controller.RemoveCommand(notifiname[i].ToString());
        }
    }

    /// <summary>
    /// 确认命令
    /// </summary>
    /// <param name="notificationName">命令名字</param>
    /// <returns></returns>
    public static bool HasCommand(string notificationName)
    {
        IController controller = Controller.GetInstance(delegate { return new Controller(); });
        return controller.HasCommand(notificationName);
    }

    /// <summary>
    /// 注册自定义代理
    /// </summary>
    /// <param name="proxy"></param>
    public static void RegisterProxy<T>() where T : class, IProxy, new()
    {
        PureMVC.IModel model = Model.GetInstance(delegate { return new Model(); });
        model.RegisterProxy(ReferencePool.Require<T>() as IProxy);
    }

    /// <summary>
    /// 是否有自定义代理
    /// </summary>
    /// <param name="proxyName">代理名字</param>
    /// <returns></returns>
    public static bool HasProxy<T>()
    {
        PureMVC.IModel model = Model.GetInstance(delegate { return new Model(); });
        return model.HasProxy(typeof(T).ToString());
    }

    /// <summary>
    /// 检索自定义代理
    /// </summary>
    /// <param name="proxyName">代理名字</param>
    /// <returns></returns>
    public static T RetrieveProxy<T>() where T:IProxy
    {
        PureMVC.IModel model = Model.GetInstance(delegate { return new Model(); });
        return (T)model.RetrieveProxy(typeof(T).ToString());
    }

    /// <summary>
    /// 移除自定义代理
    /// </summary>
    /// <param name="proxyName">代理名字</param>
    /// <returns></returns>
    public static IProxy RemoveProxy<T>()
    {
        PureMVC.IModel model = Model.GetInstance(delegate { return new Model(); });
        return model.RemoveProxy(typeof(T).ToString());
    }


    /// <summary>
    /// 注册中介者
    /// </summary>
    /// <param name="mediator"></param>
    public static void RegisterMediator<T>() where T : class, IMediator, new()
    {
        PureMVC.IView view = View.GetInstance(delegate { return new View(); });
        view.RegisterMediator(ReferencePool.Require<T>() as IMediator);
    }


    /// <summary>
    /// 移除中介者
    /// </summary>
    /// <param name="mediatorName">中介者名字</param>
    /// <returns></returns>
    public static IMediator RemoveMediator<T>()
    {
        PureMVC.IView view = View.GetInstance(delegate { return new View(); });
        return view.RemoveMediator(typeof(T).ToString());
    }
    /// <summary>
    /// 是否有中介者
    /// </summary>
    /// <param name="mediatorName">中介者名字</param>
    /// <returns></returns>
    public static bool HasMediator<T>()
    {
        PureMVC.IView view = View.GetInstance(delegate { return new View(); });
        return view.HasMediator(typeof(T).ToString());
    }
    /// <summary>
    /// 检索中介者
    /// </summary>
    /// <param name="mediatorName">中介者名字</param>
    /// <returns></returns>
    public static IMediator RetrieveMediator<T>()
    {
        PureMVC.IView view = View.GetInstance(delegate { return new View(); });
        return view.RetrieveMediator(typeof(T).ToString());
    }

    /// <summary>
    /// 广播事件
    /// </summary>
    /// <param name="notificationName">事件名</param>
    /// <param name="body">事件体</param>
    /// <param name="type">事件类型</param>
    public static void SendNotification(int notificationName, CmdType type = 0, object body = null)
    {
        NotifyObservers(new Notification(notificationName.ToString(), body, ((int)type).ToString()));
    }
    /// <summary>
    /// 广播事件
    /// </summary>
    /// <param name="notificationName">事件名</param>
    /// <param name="body">事件体</param>
    /// <param name="type">事件类型</param>
    public static void SendNotification(int notificationName,object body)
    {
        NotifyObservers(new Notification(notificationName.ToString(), body));
    }

    /// <summary>
    /// 广播事件
    /// </summary>
    /// <param name="notification">事件参数对象</param>
    public static void NotifyObservers(INotification notification)
    {
        PureMVC.IView view = View.GetInstance(delegate { return new View(); });
        view.NotifyObservers(notification);
    }

    ///////////////////////////////////原生用法///////////////////////////////////////////

    /// <summary>
    /// 注册原生代理
    /// </summary>
    /// <param name="proxyName"></param>
    public static void RegisterProxy(string proxyName)
    {
        Proxy proxy = new Proxy(proxyName);
        PureMVC.IModel model = Model.GetInstance(delegate { return new Model(); });
        model.RegisterProxy(proxy);
    }

    /// <summary>
    /// 检索原生代理
    /// </summary>
    /// <param name="proxyName">代理名字</param>
    /// <returns></returns>
    public static IProxy RetrieveProxy(string proxyName)
    {
        PureMVC.IModel model = Model.GetInstance(delegate { return new Model(); });
        return model.RetrieveProxy(proxyName);
    }



}
