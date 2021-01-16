using PureMVC;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ProjectProxy : Proxy
{
    public ProjectProxy(string proxyName, object data = null) : base(proxyName, data)
    { }

      public override void OnRegister()
    {
        base.OnRegister();
    }

    public override void OnRemove()
    {
        base.OnRemove();

    }
    

  


}



