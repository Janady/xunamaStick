using System;
using UnityEngine;
using UnityEngine.UI;

namespace View
{
    public class CellView : MonoBehaviour
    {
        private Text numText;
        private Text priceText;
        private Image image;
        private Text titleText;
        private void Start()
        {
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
                if (numText == null)
                {
                    Transform textSet = transform.FindChild("textSet");
                    numText = textSet.FindChild("Num").gameObject.GetComponent<Text>();
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
                    Transform textSet = transform.FindChild("textSet");
                    priceText = textSet.FindChild("price").gameObject.GetComponent<Text>();
                }
                priceText.text = value.ToString();
            }
        }
    }
}
