using System;
using UnityEngine;
using Mod;
using Libs.Resource;
using System.Collections;

namespace View
{
    public class GoodsListView : MonoBehaviour
    {
        private Action<int> callBack;
        private Transform tr;
        private void Start()
        {
            StartCoroutine(init());
        }
        public void setCallback(Action<int> callBack)
        {
            this.callBack = callBack;
            // StartCoroutine(init());
        }
        IEnumerator init()
        {
            tr = transform.FindChild("ScrollPanel");
            //Libs.Resource.UIManager.CloseUI(tr);
            foreach (Goods good in Goods.All())
            {
                GameObject go = UIManager.OpenUI("goodRow", null, tr);
                GoodRowView cv = go.GetComponent<GoodRowView>();
                cv.Id = good.Id;
                cv.Title = good.Title;
                cv.ImagePath = "Image/Gift";
                cv.price = good.Price;
                cv.SetCallBack(x => {
                    if (callBack != null) callBack(x);
                });
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
