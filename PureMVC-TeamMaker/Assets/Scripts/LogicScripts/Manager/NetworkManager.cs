using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using System.Threading;
using System.IO;
using System.Linq;
using Google.Protobuf;

public class NetworkManager : MonoBehaviour,IManager
{
    public string Name { get; set; }

    public const string IP = "localhost";
    public const int Port = 2345;
    static Socket clientSocket;
    ByteReader msg;
    private static Queue<Notify> notifies;

    public void Start()
    {
        msg = new ByteReader();
        notifies = new Queue<Notify>();
        clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            clientSocket.Connect(IP, Port);
            StartReceiveAsync();
        }
        catch (Exception e)
        {
            throw e;
        }
    }


    //异步给服务器发送消息
    public void StartReceiveAsync()
    {
        clientSocket.BeginReceive(msg.Data, msg.StartIndex, msg.RemainSize, SocketFlags.None, ReceiveCallback, null);
    }

    void ReceiveCallback(IAsyncResult ar)
    {
        try
        {
            if (clientSocket == null || !clientSocket.Connected) return;
            int count = clientSocket.EndReceive(ar);
            msg.ReadMsg(count, UnPackData);
        }
        catch (Exception e)
        {
            Debug.Log(e);

            clientSocket.Close();
           
        }
        StartReceiveAsync();
    }

    static void HandleResponse(Notify notify)
    {
        notifies.Enqueue(notify);
    }

    public static void SendRequest(Notify notify)
    {
        byte[] bytes = PackData(notify);
        clientSocket.Send(bytes);
    }

    /// <summary>
    /// 队列处理服务器消息
    /// </summary>
    private void Update()
    { 
            if (notifies.Count > 0)
            {
                ResponseManager.Execute(notifies.Dequeue());
            }
    }



    private void OnDestroy()
    {
        try
        {
            clientSocket.Close();
        }
        catch (Exception e)
        {
            throw e;
        }

    }


    //数据打包
    public static byte[] PackData(Notify notify)
    {
        byte[] proto = null;//主消息
        byte[] datacode = notify.message;//PB流
        proto = BitConverter.GetBytes(notify.Protocol);
        int dataAmount = proto.Length + datacode.Length;
        byte[] dataAmountcode = BitConverter.GetBytes(dataAmount);
        //数据长度+数据类型+数据
        return dataAmountcode.Concat(proto).Concat(datacode).ToArray();

    }

    //数据解包
    public static void UnPackData(byte[] notifybyte)
    {
        Notify notify = ReferencePool.Require<Notify>();
        notify.Protocol = BitConverter.ToInt32(notifybyte,0);
        notify.Feedback = BitConverter.ToInt32(notifybyte, 4);
        notify.message=notifybyte.Skip(8).ToArray();
        HandleResponse(notify);
    }

}
