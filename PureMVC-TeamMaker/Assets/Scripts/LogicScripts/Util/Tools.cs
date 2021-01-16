using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC;
using System;
using System.Reflection;

public class Tools
   {

    public static T Instantiate<T>(string path, Transform father) where T : UnityEngine.Object
    {
        return GameObject.Instantiate(Resources.Load(path), father) as T;
    }

}

