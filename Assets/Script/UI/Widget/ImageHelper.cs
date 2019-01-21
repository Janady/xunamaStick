
using UnityEngine;
using System;
namespace UI.Widget
{
    public class ImageHelper
    {
        public static Sprite GoodsImage(string path)
        {
            Debug.Log(path);
            Texture2D texture2D;
            if (path != null) texture2D = Libs.Resource.UIManager.loadImage(path, false);
            else texture2D = Libs.Resource.UIManager.loadImage("Image/Gift", true);
            if (texture2D == null) return null;
            return Libs.Resource.UIManager.GenSprite(texture2D);
        }
    }
}
