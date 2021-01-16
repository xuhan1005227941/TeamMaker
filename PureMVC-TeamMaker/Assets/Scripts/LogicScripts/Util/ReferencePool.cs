using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ReferenceCollection
{
    public Stack<Type> Items=new Stack<Type>();

    public void SetItem<T>(T obj) where T:class 
    {
        Items.Push(obj.GetType());
        
    }
    public T GetItem<T>() where T : class,new()
    {
        if (Items.Count>0)
        {
           return Items.Pop() as T;
        }
        return new T();
    }
}

   public static class ReferencePool
   {
    private static Dictionary<Type, ReferenceCollection> Pools=new Dictionary<Type, ReferenceCollection>();

    public static T Require<T>() where T:class,new()
    {   
        Type classType = typeof(T);
        ReferenceCollection referenceItem;
        if (!Pools.TryGetValue(classType,out referenceItem))
        {
            referenceItem = new ReferenceCollection();
            Pools.Add(classType, referenceItem);
        }
        T target =referenceItem.GetItem<T>();
        OnRequire(target);
        return target;
    }

    public static void Release<T>(T obj) where T:class,new()
    {
        OnRelease(obj);
        Type classType = obj.GetType();
        ReferenceCollection referenceItem;
        if (!Pools.TryGetValue(classType, out referenceItem))
        {
            referenceItem = new ReferenceCollection();
            Pools.Add(classType, referenceItem);
        }
        referenceItem.SetItem(obj);
        
    }

    static void OnRequire<T>(T obj)
    {
        if (obj is GameObject)
        {
            GameObject target = obj as GameObject;
            target.SetActive(true);
        }
        else if (obj is Transform)
        {
            Transform target = obj as Transform;
            target.gameObject.SetActive(true);
        }
        else if (obj is IReference)
        {
            IReference target = obj as IReference;
            target.OnRequire();
        }
    }

    static void OnRelease<T>(T obj)
    {
        if (obj is GameObject)
        {
            GameObject target =obj as GameObject;
            target.SetActive(false);
        }
        else if(obj is Transform)
        {
            Transform target = obj as Transform;
            target.gameObject.SetActive(false);
        }
        else if (obj is IReference)
        {
            IReference target = obj as IReference;
            target.OnRelease();
        }
    }

   }

