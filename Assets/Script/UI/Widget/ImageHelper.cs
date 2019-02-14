
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
            Folder,
            File
        }
        public static Sprite GoodsImage(string path)
        {
            Texture2D texture2D;
            if (path != null) texture2D = Libs.Resource.UIManager.loadImage(path, false);
            else texture2D = Libs.Resource.UIManager.loadImage("Image/Gift", true);
            if (texture2D == null) return null;
            return Libs.Resource.UIManager.GenSprite(texture2D);
        }
        public static Sprite DefaultImage(ImageTag tag = ImageTag.NoneImage)
        {
            string path;
            switch (tag)
            {
                case ImageTag.Folder:
                    path = "Image/folder";
                    break;
                case ImageTag.Vedio:
                    path = "Image/movie";
                    break;
                case ImageTag.Picture:
                    path = "Image/picture";
                    break;
                case ImageTag.File:
                    path = "Image/file";
                    break;
                default:
                    path = "Image/Gift";
                    break;
            }
            Texture2D texture2D = Libs.Resource.UIManager.loadImage(path, true);
            return Libs.Resource.UIManager.GenSprite(texture2D);
        }
    }
}
