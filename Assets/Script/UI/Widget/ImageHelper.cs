
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
            return Libs.Resource.UIManager.GenSprite(path);
        }
    }
}
