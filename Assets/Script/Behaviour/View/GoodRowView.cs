using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class GoodRowView : MonoBehaviour
    {
        private int id;
        private Text numText;
        private Text priceText;
        private Image image;
        private Text titleText;
        private Action<int> callBack;
        private void Start()
        {
            Button btn = gameObject.AddComponent<Button>();
            btn.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            if (callBack != null) callBack(id);
        }
        public void SetCallBack(Action<int> callBack)
        {
            this.callBack = callBack;
        }
        public string Title
        {
            set
            {
                if (titleText == null)
                {
                    titleText = transform.FindChild("title").gameObject.GetComponent<Text>();
                }
                titleText.text = value;
            }
        }
        public string ImagePath
        {
            set
            {
                if (image == null)
                {
                    image = transform.FindChild("Image").gameObject.GetComponent<Image>();
                }
                image.sprite = UI.Widget.ImageHelper.GoodsImage(value);
            }
        }
        public int Id
        {
            set
            {
                id = value;
                if (numText == null)
                {
                    numText = transform.FindChild("id").gameObject.GetComponent<Text>();
                }
                numText.text = value.ToString();
            }
        }
        public int price
        {
            set
            {
                if (priceText == null)
                {
                    priceText = transform.FindChild("price").gameObject.GetComponent<Text>();
                }
                priceText.text = value.ToString();
            }
        }
        public void setEditCallBack(Action<int> callback)
        {
            Transform tr = transform.FindChild("edit");
            setBtn(tr, callback);
        }
        public void setDeleteCallBack(Action<int> callback)
        {
            Transform tr = transform.FindChild("delete");
            setBtn(tr, callback);
        }
        private void setBtn(Transform tr, Action<int> callback)
        {
            if (callback == null || tr == null) return;
            tr.gameObject.SetActive(true);
            Button btn = tr.GetComponent<Button>();
            btn.onClick.AddListener(() =>
            {
                callback(id);
            });
        }
    }
}

