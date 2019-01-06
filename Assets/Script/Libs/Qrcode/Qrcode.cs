using System;
using ZXing;
using UnityEngine;

namespace Libs.Qrcode
{
    public class Qrcode
    {
        public const string prefixLeYaoYao = "http://m.leyaoyao.com/lyy/t/";
        public static Color32[] GenQRCodeRaw(string formatStr, int width, int height)
        {
            ZXing.QrCode.QrCodeEncodingOptions options = new ZXing.QrCode.QrCodeEncodingOptions();
            options.CharacterSet = "UTF-8";
            options.Width = width;
            options.Height = height;
            options.Margin = 1;
            BarcodeWriter barcodeWriter = new BarcodeWriter
            {
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = options
            };
            return barcodeWriter.Write(formatStr);
        }
        public static Texture2D GenQRCode(string formatStr, int width, int height)
        {
            Texture2D t = new Texture2D(width, height);
            Color32[] color32s = GenQRCodeRaw(formatStr, width, height);
            t.SetPixels32(color32s);
            t.Apply();
            return t;
        }
        public static Texture2D GenQRCodeLeYaoYao(byte[] buf)
        {
            if (null == buf || buf.Length != 8) return null;
            int ret = 0;
            foreach (byte b in buf)
            {
                ret = ret * 10 + (0xff & b);
            }
            Debug.Log(prefixLeYaoYao + ret);
            return GenQRCode(prefixLeYaoYao + ret, 256, 256); // 60014000
        }
    }
}
