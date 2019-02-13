
using UnityEngine;
using System;
namespace UI.Widget
{
    public class ImageHelper
    {
        public enum ImageTag
        {
            NoneImage,
            Vedio,
            Picture,
            Folder
        }
        public static Sprite GoodsImage(string path)
        {
            Texture2D texture2D;
            if (path != null) texture2D = Libs.Resource.UIManager.loadImage(path, false);
            else texture2D = Libs.Resource.UIManager.loadImage("Image/Gift", true);
            if (texture2D == null) return null;
            return Libs.Resource.UIManager.GenSprite(texture2D);
        }
        public static Sprite DefaultImage(ImageTag tag)
        {
            string path;
            switch (tag)
            {
                case ImageTag.Folder:
                    path = "Image/Gift";
                    break;
                default:
                    path = "Image/Gift";
                    break;
            }
            Texture2D texture2D = Libs.Resource.UIManager.loadImage(path, false);
            return Libs.Resource.UIManager.GenSprite(texture2D);
        }
    }
}
