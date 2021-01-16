using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour {

    private static PoolManager instance;
    
    public static PoolManager Instance {
        get {
            if (instance == null) {
               
                instance = new GameObject("ObjectPool").AddComponent<PoolManager>();                
            }
            return instance;
        }        
    }
    private  PoolManager(){
        Pool = new Stack<GameObject>();
        PoolDic = new Dictionary<string, GameObject>();
    }
    public Stack<GameObject> Pool;
    private Dictionary<string, GameObject> PoolDic;


    public void RegistObj(string name,GameObject obj) {
        
    }

    /// <summary>
    /// 池子式创建
    /// </summary>
    /// <param name="obj"></param>
    public GameObject CreatObj(GameObject obj) {

        GameObject poolObj;
        if (Pool.Count>0) {
            poolObj =Pool.Pop() as GameObject;
            poolObj.SetActive(true);
        } else {
            poolObj= Instantiate(obj,transform);
        }
        return poolObj;
    }

    /// <summary>
    /// 池子式消灭
    /// </summary>
    /// <param name="obj"></param>
    public void DestoryObj(GameObject obj) {
        obj.SetActive(false);
        Pool.Push(obj);
    }
}
