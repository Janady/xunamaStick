using UnityEngine;
using UnityEngine.UI;
using System;
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
        // image.sprite = UIManager.GenSprite(UIManager.loadImage(_good.ImagePath, false));
    }
    void ChangeImage()
    {
        Libs.Api.ChooseFileApi.chooseImageFile();
    }
    void OnClick()
    {
        Destroy(gameObject);
        _good.Title = nameField.text;
        _good.Price = int.Parse(priceField.text);
        _good.update();
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
}
