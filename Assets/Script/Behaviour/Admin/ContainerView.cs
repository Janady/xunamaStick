using UnityEngine;
using System.Collections;
using Libs.Resource;
using View;
using Mod;

public class ContainerView : MonoBehaviour
{
    private ContainerListView list;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(initNav());
        list = transform.FindChild("GoodsList").GetComponent<ContainerListView>();
        if (list != null) list.setCallback(x=> {
            GameObject go = UIManager.OpenUI(Config.UI.UIPath.GoodsSelectPanel);
            GoodsSelectView gsv = go.GetComponent<GoodsSelectView>();
            gsv.SetCabinet(Cabinet.GetById(x), (retid)=>{
                Cabinet cabinet = Cabinet.GetById(x);
                cabinet.GoodsId = retid;
                cabinet.update();
                Libs.Resource.GameObjectManager.Destroy(go);
                Refresh();
            });
            gsv.setActionCallback(() => {
                Libs.Resource.GameObjectManager.Destroy(go);
                Refresh();
            });
        });
    }
    IEnumerator initNav()
    {
        NavView nv = transform.FindChild("nav").GetComponent<NavView>();
        nv.Title = "货柜管理";
        nv.setBtn1("一键开锁", ()=> {
            Service.LockingPlateService.Instance().OpenAll();
        });
        nv.setBtn2("一键补货", ()=> {
            //GameObject go = UIManager.OpenUI(Config.UI.UIPath.LoadingPanel);
            Service.GoodsService.Instance().Replenishment();
            Refresh();
            //Destroy(go);
        });
        yield return new WaitForEndOfFrame();
    }

    // Update is called once per frame
    private void Refresh()
    {
        list.Refresh();
    }
}
