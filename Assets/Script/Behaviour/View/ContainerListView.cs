using System;
using UnityEngine;
using Mod;
using Libs.Resource;
using System.Collections;

namespace View
{
    public class ContainerListView : MonoBehaviour
    {
        private Action<int> callBack;

        public void setCallback(Action<int> callBack)
        {
            this.callBack = callBack;
            StartCoroutine(init());
        }
        IEnumerator init()
        {
            int i = 0;
            Transform tr = transform.FindChild("ScrollPanel");
            UIManager.CloseUI(tr);
            yield return new WaitForEndOfFrame();
            foreach (Cabinet cabinet in Cabinet.All())
            {
                GameObject go = UIManager.OpenUI("cell", null, tr, i++);
                CellView cv = go.GetComponent<CellView>();
                cv.Id = cabinet.Id;
                cv.Num = cabinet.Num;
                Goods good = cabinet.Good();
                if (good == null)
                {
                    cv.Title = "请添加商品";
                    cv.price = 0;
                    cv.ImagePath = "Image/HeartGrey";
                    cv.setCellTag(CellView.CellTag.None);
                }
                else
                {
                    cv.Title = good.Title;
                    cv.price = good.Price;
                    cv.ImagePath = good.ImagePath;
                    // set tag
                    CellView.CellTag tag = CellView.CellTag.None;
                    if (!cabinet.Enabled) tag = CellView.CellTag.Disable;
                    else if (cabinet.Count <= 0) tag = CellView.CellTag.Sold;
                    cv.setCellTag(tag);
                }
                cv.SetCallBack(x => {
                    if (callBack != null) callBack(x);
                });
                yield return new WaitForEndOfFrame();
            }
        }
        public void Refresh()
        {
            StartCoroutine(init());
        }
    }
}
