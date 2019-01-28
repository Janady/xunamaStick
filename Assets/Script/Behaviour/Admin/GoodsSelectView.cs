using UnityEngine;
using UnityEngine.UI;
using System;
using Mod;

public class GoodsSelectView : MonoBehaviour
{
    private Action<int> callBack;
    private Action actionCallBack;
    private Cabinet _cabinet;
    // Use this for initialization
    void Start()
    {
        // initGoodsList();
        // initNav(Goods.GetGood(_id));
        Transform tr = transform.FindChild("Content").FindChild("nav");
        Button action = tr.FindChild("action").GetComponent<Button>();
        action.onClick.AddListener(ActionOnClick);
        action.gameObject.SetActive(true);
        Button use = tr.FindChild("use").GetComponent<Button>();
        use.gameObject.SetActive(true);
        use.onClick.AddListener(UseOnClick);
    }
    private void ActionOnClick()
    {
        if (_cabinet == null) return;
        if (_cabinet.Count > 0)
        {
            _cabinet.clear();
        }
        else
        {
            _cabinet.Count = 1;
            _cabinet.update();
        }
        if (actionCallBack != null) actionCallBack();
    }
    private void UseOnClick()
    {
        bool useless = !_cabinet.Enabled;
        _cabinet.Enabled = useless;
        _cabinet.update();
        if (actionCallBack != null) actionCallBack();
    }
    private void initNav(Goods good)
    {
        if (good == null || _cabinet == null) return;
        Transform tr = transform.FindChild("Content").FindChild("nav");

        // set button
        Button action = tr.FindChild("action").GetComponent<Button>();
        Text actionText = tr.FindChild("action").FindChild("Text").GetComponent<Text>();
        if (_cabinet.Count > 0)
        {
            actionText.text = "下架";
        }
        else
        {
            actionText.text = "补货";
        }

        // set enabke
        Button use = tr.FindChild("use").GetComponent<Button>();
        Text usetitle = tr.FindChild("use").FindChild("Text").GetComponent<Text>();
        bool useless = !_cabinet.Enabled;
        usetitle.text = useless ? "修复启用" : "损坏禁用";

        // set count
        /*
        Text count = tr.FindChild("count").GetComponent<Text>();
        count.gameObject.SetActive(true);
        count.text = _cabinet.Count.ToString();
        */
    }

    private void initGoodsList()
    {
        View.GoodsListView list = transform.FindChild("Content").FindChild("GoodsList").GetComponent<View.GoodsListView>();
        if (list != null) list.setCallback(callBack);
        list.ExceptId = _cabinet.GoodsId;
    }
    public void setCallback(Action<int> callBack)
    {
        this.callBack = callBack;
        initGoodsList();
    }
    public void setActionCallback(Action callBack)
    {
        this.actionCallBack = callBack;
    }
    public void SetCabinet(Cabinet cabinet)
    {
        _cabinet = cabinet;
        initNav(Goods.GetGood(cabinet.GoodsId));
    }
}
