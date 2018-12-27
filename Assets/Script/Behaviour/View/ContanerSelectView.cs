using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class ContanerSelectView : MonoBehaviour
{
    private Action<int> callBack;
    // Use this for initialization
    void Start()
    {
        View.ContainerListView list = transform.FindChild("GoodsList").GetComponent<View.ContainerListView>();
        if (list != null) list.setCallback(callBack);
    }
    public void setCallback(Action<int> callBack)
    {
        this.callBack = callBack;
        View.GoodsListView list = gameObject.GetComponent<View.GoodsListView>();
        if (list != null) list.setCallback(callBack);
    }
}
