using UnityEngine;
using UnityEngine.UI;
using System;
using Libs.Event;
using Libs.Resource;
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
    void Start()
    {
        tr = transform.FindChild("bg");
        nameField = tr.FindChild("nameField").GetComponent<InputField>();
        priceField = tr.FindChild("InputField (1)").GetComponent<InputField>();
        image = tr.FindChild("picture").GetComponent<Image>();
        Button ibtn = image.gameObject.AddComponent<Button>();
        ibtn.onClick.AddListener(ChangeImage);
        Button btn = tr.FindChild("Button").GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
        if (_good == null) return;
        nameField.text = _good.Title;
        priceField.text = _good.Price.ToString();
        imgStr = _good.ImagePath;
        if (imgStr != null) image.sprite = UIManager.GenSprite(UIManager.loadImage(imgStr, false));
    }
    private void OnDisable()
    {
        EventMgr.Instance.RemoveEvent(EventNameData.ChooseFile, OnChooseFile);
    }
    private void OnEnable()
    {
        EventMgr.Instance.AddEvent(EventNameData.ChooseFile, OnChooseFile);
    }
    void ChangeImage()
    {
        Libs.Api.ChooseFileApi.chooseImageFile();
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
        Destroy(gameObject);
        if (callBack != null) callBack();
    }
    public Action CallBack
    {
        set
        {
            callBack = value;
        }
    }
    public Goods Good
    {
        set
        {
            _good = value;
        }
    }
    /*
     * event handler
     */
    private void OnChooseFile(object dispatcher, string eventName, object value)
    {
        imgStr = value as string;
        image.sprite = UIManager.GenSprite(UIManager.loadImage(imgStr, false));
    }
}
