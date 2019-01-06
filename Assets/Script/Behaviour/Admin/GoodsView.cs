using UnityEngine;
using System.Collections;
using System;
using Libs.Resource;
using View;
using Mod;

public class GoodsView : MonoBehaviour
{
    private GoodsListView list;
    private NavView nav;
    // Use this for initialization
    void Start()
    {
        nav = transform.FindChild("nav").GetComponent<NavView>();
        nav.setBtn1("新增礼品", () => {
            GameObject go = UIManager.OpenUI(Config.UI.UIPath.EditGoodsPanel);
            EditGoodsView ev = go.AddComponent<EditGoodsView>();
            ev.CallBack = (Refresh);
        });
        list = transform.FindChild("Content").FindChild("GoodsList").GetComponent<GoodsListView>();
        list.setCallback(null, x=> {
            GameObject go = UIManager.OpenUI(Config.UI.UIPath.EditGoodsPanel);
            EditGoodsView ev = go.AddComponent<EditGoodsView>();
            ev.Good = Goods.GetGood(x);
            ev.CallBack = (Refresh);
        }, x => {
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
