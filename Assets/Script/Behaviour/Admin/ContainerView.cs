using UnityEngine;
using System;
using Libs.Resource;
using View;
using Mod;

public class ContainerView : MonoBehaviour
{
    private ContainerListView list;
    // Use this for initialization
    void Start()
    {
        list = transform.FindChild("GoodsList").GetComponent<ContainerListView>();
        if (list != null) list.setCallback(x=> {
            GameObject go = UIManager.OpenUI(Config.UI.UIPath.GoodsSelectPanel);
            GoodsSelectView gsv = go.GetComponent<GoodsSelectView>();
            gsv.SetId(Cabinet.GetById(x).GoodsId);
            gsv.SetCabinet(Cabinet.GetById(x));
            gsv.setCallback((retid)=>{
                Cabinet cabinet = Cabinet.GetById(x);
                cabinet.GoodsId = retid;
                cabinet.update();
                Destroy(go);
                Refresh();
            });
            gsv.setActionCallback(() => {
                Destroy(go);
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
