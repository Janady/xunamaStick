using UnityEngine;
using System.Collections;
using System.IO;
using Libs.Resource;
using View;
using Mod;
using SimpleJSON;

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
        nav.setBtn2("文件导入", () => {
            UI.Widget.FileManager.openf("/Users/janady/test", onLoadFile, "*.json");
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
    private void onLoadFile(string filename)
    {
        FileInfo fi = new FileInfo(filename);
        StreamReader stream = fi.OpenText();
        string jsonstr = stream.ReadToEnd();

        JSONNode N = JSON.Parse(jsonstr);
        foreach (JSONNode goodsJson in N["goods"])
        {
            Goods g = new Goods
            {
                Sku = goodsJson["SKU"].ToString(),
                Title = goodsJson["name"].ToString(),
                Price = (int)goodsJson["price"],
                ImagePath = fi.Directory.FullName + "/" + goodsJson["Image"].Value.ToString(),
                Type = "999"
            };
            //Debug.Log(g);
            g.insert();
        }
        stream.Close();
        stream.Dispose();
        Refresh();
    }
}
