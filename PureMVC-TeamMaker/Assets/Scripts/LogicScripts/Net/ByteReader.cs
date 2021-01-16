using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;
using System.Linq;
using System.IO;
using Google.Protobuf;

public class ByteReader 
{
    private byte[] data = new byte[1024];//数据
    private int startIndex = 0;//当前存取了多少个字节的数据在数组里面
    public byte[] Data {
        get { return data; }
    }

    public int StartIndex {
        get { return startIndex; }
    }

    public int RemainSize {
        get { return data.Length - startIndex; }
    }

    /// <summary>
    /// 解析数据
    /// </summary>
    public void ReadMsg(int newDataAmount, Action<byte[]> Callback)
    {
        startIndex += newDataAmount;
        while (true)
        {
            if (startIndex <= 4)
            {
                return;
            }

            int count = BitConverter.ToInt32(data, 0);
            if (startIndex - 4 >= count)
            {
                byte[] targetArray = data.Skip(4).Take(count).ToArray();
                Callback(targetArray);
                Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);
                startIndex -= (count + 4);
            }
            else
            {
                break;
            }


        }

    }


}
