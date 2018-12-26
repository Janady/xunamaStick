using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Libs.Qrcode;
using Libs.Resource;

public class PayView : MonoBehaviour
{
    public string qrcodeId
    {
        set
        {
            Texture2D texture = Qrcode.GenQRCodeLeYaoYao(value);
            Image image = transform.FindChild("qrcode").gameObject.GetComponent<Image>();
            image.sprite = UIManager.GenSprite(texture);
        }
    }
    public int amount
    {
        set
        {
            Text text = transform.FindChild("amount").gameObject.GetComponent<Text>();
            text.text = value.ToString();
        }
    }
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}
