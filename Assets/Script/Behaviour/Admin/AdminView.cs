using UnityEngine;
using UnityEngine.UI;
using Libs.Resource;
using System;
using System.Collections;

public class AdminView : MonoBehaviour
{
    private NavView nv;
    private Transform contentTr;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(initNav());
        contentTr = transform.FindChild("Content");
        if (contentTr == null) throw new Exception("No content");
        
        GameObject fiancial = contentTr.FindChild("HomeContent").FindChild("fiancial").gameObject;
        EventTriggerListener.Get(fiancial).onClick = OnFiancialClick;
        //     
        GameObject device = contentTr.FindChild("HomeContent").FindChild("device").gameObject;
        EventTriggerListener.Get(device).onClick = OnDeviceClick;

        GameObject connect = contentTr.FindChild("HomeContent").FindChild("goods").gameObject;
        EventTriggerListener.Get(connect).onClick = OnConnectClick;

        GameObject container = contentTr.FindChild("HomeContent").FindChild("container").gameObject;
        EventTriggerListener.Get(container).onClick = OnContainerClick;

        GameObject version = contentTr.FindChild("HomeContent").FindChild("version").gameObject;
        EventTriggerListener.Get(version).onClick = OnVersionClick;

        GameObject ad = contentTr.FindChild("HomeContent").FindChild("ad").gameObject;
        EventTriggerListener.Get(ad).onClick = OnAdClick;
    }

    IEnumerator initNav()
    {
        NavView nv = transform.FindChild("nav").GetComponent<NavView>();
        nv.Title = "货柜管理";
        nv.setBtn1("一键开锁", () => {
            Service.LockingPlateService.Instance().OpenAll();
        });
        nv.setBtn2("一键补货", () => {
            Service.GoodsService.Instance().Replenishment();
        });
        yield return new WaitForEndOfFrame();
    }

    #region button click event
    private void OnFiancialClick(GameObject go)
    {
        UIManager.OpenUI(Config.UI.UIPath.FinancialPanel);
    }
    private void OnDeviceClick(GameObject go)
    {
        UIManager.OpenUI(Config.UI.UIPath.DevicePanel);
    }
    private void OnConnectClick(GameObject go)
    {
        UIManager.OpenUI(Config.UI.UIPath.GoodsPanel);
    }
    private void OnContainerClick(GameObject go)
    {
        UIManager.OpenUI(Config.UI.UIPath.ContainerPanel);
    }
    private void OnVersionClick(GameObject go)
    {
        UIManager.OpenUI(Config.UI.UIPath.VersionPanel);
    }
    private void OnAdClick(GameObject go)
    {
        UIManager.OpenUI(Config.UI.UIPath.AdPanel);
    }
    #endregion
}
