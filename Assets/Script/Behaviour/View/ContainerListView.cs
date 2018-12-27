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
            foreach (Cabinet cabinet in Cabinet.All())
            {
                GameObject go = UIManager.OpenUI("cell", null, tr);
                CellView cv = go.GetComponent<CellView>();
                cv.Num = cabinet.Num;
                cv.Title = cabinet.Good().Title;
                cv.ImagePath = "Image/Gift";
                cv.price = cabinet.Good().Price;
                cv.SetCallBack(x => {
                    if (callBack != null) callBack(x);
                });
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
