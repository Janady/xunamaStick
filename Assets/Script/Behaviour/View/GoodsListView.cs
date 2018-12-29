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
        private Action<int> editCallBack;
        private Action<int> deleteCallBack;
        private int _exceptId = 0;
        private Transform tr;
        private void Start()
        {
            tr = transform.FindChild("ScrollPanel");
            StartCoroutine(init(tr));
        }
        public int ExceptId
        {
            set
            {
                _exceptId = value;
            }
        }
        public void setCallback(Action<int> callBack, Action<int> editCallBack = null, Action<int> deleteCallBack = null)
        {
            this.callBack = callBack;
            this.editCallBack = editCallBack;
            this.deleteCallBack = deleteCallBack;
        }
        IEnumerator init(Transform tr)
        {
            foreach (Goods good in Goods.All())
            {
                if (good.Id != _exceptId)
                {
                    GameObject go = UIManager.OpenUI("goodRow", null, tr);
                    GoodRowView cv = go.GetComponent<GoodRowView>();
                    cv.Id = good.Id;
                    cv.Title = good.Title;
                    cv.ImagePath = "Image/Gift";
                    cv.price = good.Price;
                    if (callBack != null) cv.SetCallBack(callBack);
                    if (editCallBack != null) cv.setEditCallBack(editCallBack);
                    if (deleteCallBack != null) cv.setDeleteCallBack(deleteCallBack);
                    yield return new WaitForEndOfFrame();
                }
            }
        }
        public void Refresh()
        {
            UIManager.CloseUI(tr);
            StartCoroutine(init(tr));
        }
    }
}
