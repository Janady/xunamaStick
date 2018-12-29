﻿using UnityEngine;
using System.Collections;
using System;
using Libs.Resource;
using View;
using Mod;

public class GoodsView : MonoBehaviour
{
    private GoodsListView list;
    // Use this for initialization
    void Start()
    {
        list = transform.FindChild("Content").FindChild("GoodsList").GetComponent<GoodsListView>();
        list.setCallback(null, x=>Debug.Log(x), x => {
            Goods g = Goods.GetGood(x);
            string text = "确定删除\'" + g.Title + "\'吗?";
            UI.Widget.CommonTips.showDelete(text, ()=>
            {
                g.delete();
                Refresh();
            });
        });
    }

    // Update is called once per frame
    private void Refresh()
    {
        list.Refresh();
    }
}
