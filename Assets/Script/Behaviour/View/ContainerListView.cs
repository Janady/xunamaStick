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
                cv.Title = cabinet.Good().Title;
                cv.ImagePath = "Image/Gift";
                cv.price = cabinet.Good().Price;
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
