using UnityEngine;
using UnityEngine.UI;
using System;
using Mod;

public class GoodsSelectView : MonoBehaviour
{
    private Action<int> callBack;
    private int _id = 0;
    // Use this for initialization
    void Start()
    {
        initGoodsList();
        initNav(Goods.GetGood(_id));
    }
    private void initNav(Goods good)
    {
        if (good == null) return;
        Transform tr = transform.FindChild("Content").FindChild("nav");
        Text title = tr.FindChild("title").GetComponent<Text>();
        title.text = good.Title;
    }

    private void initGoodsList()
    {
        View.GoodsListView list = transform.FindChild("Content").FindChild("GoodsList").GetComponent<View.GoodsListView>();
        if (list != null) list.setCallback(callBack);
        list.ExceptId = _id;
    }
    public void setCallback(Action<int> callBack)
    {
        this.callBack = callBack;
        initGoodsList();
    }
    public void SetId(int id)
    {
        _id = id;
    }
}
