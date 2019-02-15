using UnityEngine;
using UnityEngine.UI;
using System;
using Mod;
public class EditGoodsView : MonoBehaviour
{
    private Goods _good;
    private Transform tr;
    private InputField nameField;
    private InputField priceField;
    private Image image;
    private Action callBack;
    private string imgStr;
    // Use this for initialization
    private void Start()
    {
        tr = transform.FindChild("bg");
        //Button ibtn = tr.FindChild("picture").gameObject.AddComponent<Button>();
        //ibtn.onClick.AddListener(ChangeImage);
        EventTriggerListener.Get(tr.FindChild("picture").gameObject).onClick = ChangeImage;
        Button btn = tr.FindChild("Button").GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }
    private void init()
    {
        tr = transform.FindChild("bg");
        nameField = tr.FindChild("nameField").GetComponent<InputField>();
        priceField = tr.FindChild("InputField (1)").GetComponent<InputField>();
        image = tr.FindChild("picture").GetComponent<Image>();
        if (_good == null)
        {
            nameField.text = "";
            priceField.text = "";
            imgStr = null;
            image.sprite = Libs.Resource.UIManager.GenSprite(imgStr);
        }
        else
        {
            nameField.text = _good.Title;
            priceField.text = _good.Price.ToString();
            imgStr = _good.ImagePath;
            image.sprite = Libs.Resource.UIManager.GenSprite(imgStr);
        }
    }
    void ChangeImage(GameObject go)
    {
        UI.Widget.FileManager.openf(Config.Constant.UsbPath, (string s)=> {
            imgStr = FileUtil.storeFile(s, Config.Constant.ImagePath);
            image.sprite = Libs.Resource.UIManager.GenSprite(imgStr);
        }, "*.png", "*.jpg", "*.jpeg", "*.bmp");
    }
    void OnClick()
    {
        int defaultPrice = 2;
        int.TryParse(priceField.text, out defaultPrice);
        if (_good == null)
        {
            _good = new Goods
            {
                Title = nameField.text,
                Price = defaultPrice,
                ImagePath = imgStr
            };
            _good.insert();
        }
        else
        {
            _good.Title = nameField.text;
            _good.Price = defaultPrice;
            _good.ImagePath = imgStr;
            _good.update();
        }
        Libs.Resource.GameObjectManager.Destroy(gameObject);
        if (callBack != null) callBack();
    }
    public void setGoodAndCallback(Goods goods, Action action)
    {
        _good = goods;
        callBack = action;
        init();
    }
}
