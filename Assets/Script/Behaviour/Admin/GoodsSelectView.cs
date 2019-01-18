using UnityEngine;
using UnityEngine.UI;
using System;
using Mod;

public class GoodsSelectView : MonoBehaviour
{
    private Action<int> callBack;
    private Action actionCallBack;
    private int _id = 0;
    private Cabinet _cabinet;
    // Use this for initialization
    void Start()
    {
        initGoodsList();
        initNav(Goods.GetGood(_id));
    }
    private void initNav(Goods good)
    {
        if (good == null || _cabinet == null) return;
        Transform tr = transform.FindChild("Content").FindChild("nav");

        // set button
        Button action = tr.FindChild("action").GetComponent<Button>();
        action.gameObject.SetActive(true);
        Text actionText = tr.FindChild("action").FindChild("Text").GetComponent<Text>();
        if (_cabinet.Count > 0)
        {
            action.onClick.AddListener(() => {
                _cabinet.clear();
                if (actionCallBack != null) actionCallBack();
            });
            actionText.text = "下架";
        }
        else
        {
            action.onClick.AddListener(() => {
                _cabinet.Count = 1;
                _cabinet.update();
                if (actionCallBack != null) actionCallBack();
            });
            actionText.text = "补货";
        }

        // set enabke
        Button use = tr.FindChild("use").GetComponent<Button>();
        use.gameObject.SetActive(true);
        Text usetitle = tr.FindChild("use").FindChild("Text").GetComponent<Text>();
        bool useless = !_cabinet.Enabled;
        usetitle.text = useless ? "修复启用" : "损坏禁用";
        use.onClick.AddListener(() => {
            _cabinet.Enabled = useless;
            _cabinet.update();
            if (actionCallBack != null) actionCallBack();
        });

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
        list.ExceptId = _id;
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
    public void SetId(int id)
    {
        _id = id;
    }
    public void SetCabinet(Cabinet cabinet)
    {
        _cabinet = cabinet;
    }
}
