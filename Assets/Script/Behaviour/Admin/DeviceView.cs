using UnityEngine;
using System.Collections;

public class DeviceView : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(initNav());

    }

    IEnumerator initNav()
    {
        NavView nv = transform.FindChild("nav").GetComponent<NavView>();
        nv.Title = "货柜管理";
        nv.setBtn1("yi、、一键开锁", () => {
            Service.LockingPlateService.Instance().OpenAll();
        });
        nv.setBtn2("一键补货", () => {
            Service.GoodsService.Instance().Replenishment();
        });
        yield return new WaitForEndOfFrame();
    }
}
