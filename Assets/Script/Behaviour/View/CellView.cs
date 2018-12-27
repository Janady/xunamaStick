using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class CellView : MonoBehaviour
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
                Texture2D texture2D = Libs.Resource.UIManager.loadImage(value, true);
                image.sprite = Libs.Resource.UIManager.GenSprite(texture2D);
            }
        }
        public int Num
        {
            set
            {
                id = value;
                if (numText == null)
                {
                    numText = transform.FindChild("number").gameObject.GetComponent<Text>();
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
    }
}
