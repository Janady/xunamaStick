using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Libs.Qrcode;
using Libs.Resource;
using Service;

public class PayView : MonoBehaviour
{
    public int amount
    {
        set
        {
            Text text = transform.FindChild("amount").gameObject.GetComponent<Text>();
            text.text = value.ToString();
        }
    }
    private void OnEnable()
    {
        byte[] identity = SerialIOService.GetInstance().PayIdentity;
        Image image = transform.FindChild("qrcode").gameObject.GetComponent<Image>();
        if (null == identity) return;
        Texture2D texture = Qrcode.GenQRCodeLeYaoYao(identity);
        image.sprite = UIManager.GenSprite(texture);
    }
}
