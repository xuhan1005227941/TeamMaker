using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using DG.Tweening;
public class Timer
{

    public static void StartTimer(int time,Action hander)
    {
        DOTween.To(delegate { return 0; },delegate { }, 1, time).OnComplete(delegate { hander(); });
    }





}
