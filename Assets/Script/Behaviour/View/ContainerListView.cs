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
        private Transform tr;
        private void Start()
        {
            tr = transform.FindChild("ScrollPanel");
            StartCoroutine(init(tr));
        }
        public void setCallback(Action<int> callBack)
        {
            this.callBack = callBack;
            // StartCoroutine(init());
        }
        IEnumerator init(Transform tr)
        {
            //Libs.Resource.UIManager.CloseUI(tr);
            foreach (Cabinet cabinet in Cabinet.All())
            {
                GameObject go = UIManager.OpenUI("cell", null, tr);
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
                    cv.ImagePath = "Image/Gift";
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
            UIManager.CloseUI(tr);
            StartCoroutine(init(tr));
        }
    }
}
