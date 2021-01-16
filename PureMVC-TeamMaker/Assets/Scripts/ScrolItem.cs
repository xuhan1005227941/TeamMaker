using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrolItem : MonoBehaviour
{
    int itemIndex;
    public ScrollType scrollType;
    public int itemcount;
    public bool isForward;
    public float offset;
    public float startValue;
    public float endValue;
    
    public Vector3 normalpos;

    private void Start()
    {
        itemIndex =transform.GetSiblingIndex()+1;
        normalpos = transform.position;
    }
    void Update()
    {
        switch (scrollType)
        {
            case ScrollType.Horizontal:
                if (isForward)
                {
                    LeftScroll();
                }
                else
                {
                    RightScroll();
                }
                break;
            case ScrollType.Vertical:
                if (isForward)
                {
                    DownScroll();
                   
                }
                else
                {
                    UpScroll();
                }
                break;
        }
        
    }

    public void SetNormalPos(int balance)
    {
        
        int rate = itemIndex - balance;
  
        if (rate<1)
        {
            rate = itemcount + rate;
        }
        else if (rate > itemcount)
        {
            rate = rate - itemcount;
        }

        switch (scrollType)
        {
            case ScrollType.Horizontal:
            transform.position = new Vector3(
            startValue + (rate - 1) * offset,
            normalpos.y,
            normalpos.z
            );
                break;
            case ScrollType.Vertical:
            transform.position = new Vector3(
            normalpos.x,
            startValue - (rate - 1) * offset,
            normalpos.z
            );
                break;
        }
        
    }

    void LeftScroll()
    {
        if (transform.position.x < startValue - offset)
        {
            GetComponentInParent<EndlessScroll>().SetNormalPos(itemIndex - itemcount);
        }
    }

    void RightScroll()
    {
        if (transform.position.x > endValue + offset)
        {
            GetComponentInParent<EndlessScroll>().SetNormalPos(itemIndex - 1);
        }
    }


    void UpScroll()
    {
        if (transform.position.y > startValue + offset)
        {
            GetComponentInParent<EndlessScroll>().SetNormalPos(itemIndex - itemcount);
        }
    }

    void DownScroll()
    {
        if (transform.position.y < endValue - offset)
        {
            GetComponentInParent<EndlessScroll>().SetNormalPos(itemIndex-1);
        }
    }
}
