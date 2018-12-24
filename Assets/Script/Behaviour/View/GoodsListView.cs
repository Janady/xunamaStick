using System;
using UnityEngine;
using Mod;
using Libs.Resource;

namespace View
{
    public class GoodsListView : MonoBehaviour
    {
        private Transform tr;
        private void Start()
        {
            tr = transform.FindChild("GridPanel");
            initGoods();
        }
        private void initGoods()
        {
            foreach (Cabinet cabinet in Cabinet.All())
            {
                Debug.Log(cabinet.ToString());
                GameObject go = UIManager.OpenUI("cell", null, tr);
                CellView cv = go.GetComponent<CellView>();
                cv.Num = cabinet.Num;
                cv.Title = cabinet.ToString();
                cv.ImagePath = "Image/usedLips";
                cv.price = cabinet.Good().Price;
                break;
            }
        }
    }
}
