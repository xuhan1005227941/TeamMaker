using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScrollType
{
    Horizontal,
    Vertical
}
public class EndlessScroll : MonoBehaviour
{
    public Transform Content;
    public ScrollType scrollType;
    int childCount;
    Vector2 startPos;//初始坐标
    Vector2 endPos;//结束坐标
    bool isForward;//是否正向滑动(右或者下)
    float valueForward;//滑动设定值
    public virtual void Start()
    {
        childCount = Content.childCount;
        startPos = Content.GetChild(0).position;
        endPos = Content.GetChild(Content.childCount - 1).position;
        for (int i = 0; i < Content.childCount; i++)
        {
            Content.GetChild(i).gameObject.AddComponent<ScrolItem>();
        }
        switch (scrollType)
        {
            case ScrollType.Horizontal:
                HorizontalInit();
                break;
            case ScrollType.Vertical:
                VerticalInit();
                break;
            default:
                break;
        }

    }

    void HorizontalInit()
    {
        float offset = Content.GetChild(1).position.x - Content.GetChild(0).position.x;

        valueForward = Content.position.x;
        foreach (ScrolItem item in GetComponentsInChildren<ScrolItem>())
        {
            item.itemcount = childCount;
            item.offset = offset;
            item.startValue = startPos.x;
            item.endValue = endPos.x;
            item.scrollType = scrollType;
        }
    }

    void VerticalInit()
    {
        float offset = Content.GetChild(0).position.y - Content.GetChild(1).position.y;
        
        valueForward = Content.position.y;
        foreach (ScrolItem item in GetComponentsInChildren<ScrolItem>())
        {
            item.itemcount = childCount;
            item.offset = offset;
            item.startValue = startPos.y;
            item.endValue = endPos.y;
            item.scrollType = scrollType;
        }
    }

    void SetForward()
    {
        foreach (ScrolItem item in GetComponentsInChildren<ScrolItem>())
        {
            item.isForward = isForward;
        }
    }
    
    public void SetNormalPos(int balance)
    {
        foreach (ScrolItem item in GetComponentsInChildren<ScrolItem>())
        {
            item.SetNormalPos(balance);
        }
    }
    public virtual void Update()
    {
        switch (scrollType)
        {
            case ScrollType.Horizontal:
                float curX = Content.position.x;
                if (curX - valueForward > 0)
                {
                    isForward = false;
                }
                else
                {
                    isForward = true;
                }
                SetForward();
                valueForward = curX;
                break;
            case ScrollType.Vertical:
                float curY = Content.position.y;
                if (curY - valueForward > 0)
                {
                    isForward = false;
                }
                else
                {
                    isForward = true;
                }
                SetForward();
                valueForward = curY;
                break;
            default:
                break;
        }
        
    }


}
