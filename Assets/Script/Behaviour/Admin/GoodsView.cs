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
            EditGoodsView ev = go.GetComponent<EditGoodsView>();
            if (ev == null) ev = go.AddComponent<EditGoodsView>();
            ev.setGoodAndCallback(null, Refresh);
        });
        nav.setBtn2("文件导入", () => {
            UI.Widget.FileManager.openf(Config.Constant.UsbPath, onLoadFile, "*.json", "*.txt");
        });
        list = transform.FindChild("Content").FindChild("GoodsList").GetComponent<GoodsListView>();
        list.setCallback(null, x=> {
            GameObject go = UIManager.OpenUI(Config.UI.UIPath.EditGoodsPanel);
            EditGoodsView ev = go.GetComponent<EditGoodsView>();
            if (ev == null) ev = go.AddComponent<EditGoodsView>();
            ev.setGoodAndCallback(Goods.GetGood(x), Refresh);
        }, x => {
            Goods g = Goods.GetGood(x);
            string text = "确定删除\'" + g.Title + "\'吗?";
            UI.Widget.CommonTips.showDelete(text, ()=>
            {
                g.delete();
                Refresh();
            });
        });
        Debug.Log("on Awake");
    }
    private void OnEnable()
    {
        Refresh();
    }
    // Update is called once per frame
    private void Refresh()
    {
        if (list != null) list.Refresh();
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
                Sku = goodsJson["SKU"].Value,
                Title = goodsJson["name"].Value,
                Price = int.Parse(goodsJson["price"].Value),
                ImagePath = FileUtil.storeFile(fi.Directory.FullName + "/" + goodsJson["Image"].Value.ToString(), Config.Constant.ImagePath),
                Type = "999"
            };
            g.insert();
        }
        stream.Close();
        stream.Dispose();
        Refresh();
    }
}
