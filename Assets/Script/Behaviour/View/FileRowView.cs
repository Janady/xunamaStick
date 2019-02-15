using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace View
{
    public class FileRowView : MonoBehaviour
    {
        private Text titleText;
        private Image hintImage;
        private Action<FileInfo, DirectoryInfo> callBack;
        private FileInfo fileInfo;
        private DirectoryInfo directoryInfo;


        private void Start()
        {
            Button btn = gameObject.AddComponent<Button>();
            btn.onClick.AddListener(OnClick);
        }
        private void OnClick()
        {
            if (callBack != null) callBack(fileInfo, directoryInfo);
        }
        private Text TitleText
        {
            get
            {
                if (titleText == null)
                {
                    titleText = transform.FindChild("title").gameObject.GetComponent<Text>();
                }
                return titleText;
            }
        }
        private Image HintImage
        {
            get
            {
                if (hintImage == null)
                {
                    Transform tr = transform.FindChild("Image");
                    tr.gameObject.SetActive(true);
                    hintImage = tr.GetComponent<Image>();
                }
                return hintImage;
            }
        }
        public void setValueAndCallback(FileInfo fi, DirectoryInfo di, Action<FileInfo, DirectoryInfo> action)
        {
            callBack = action;
            if (fi != null)
            {
                TitleText.text = fi.Name;
                switch (fi.Extension)
                {
                    case ".mp4":
                    case ".flv":
                    case ".mov":
                    case ".qt":
                    case ".avi":
                        HintImage.sprite = UI.Widget.ImageHelper.DefaultImage(UI.Widget.ImageHelper.ImageTag.Vedio);
                        break;
                    case ".png":
                    case ".jpg":
                    case ".jpeg":
                    case ".bmp":
                        HintImage.sprite = UI.Widget.ImageHelper.DefaultImage(UI.Widget.ImageHelper.ImageTag.Picture); // GoodsImage(fi.FullName);
                        break;
                    default:
                        HintImage.sprite = UI.Widget.ImageHelper.DefaultImage(UI.Widget.ImageHelper.ImageTag.File);
                        break;
                }
            }
            else if (di != null)
            {
                TitleText.text = di.Name;
                HintImage.sprite = UI.Widget.ImageHelper.DefaultImage(UI.Widget.ImageHelper.ImageTag.Folder);
            }
            fileInfo = fi;
            directoryInfo = di;
        }
    }
}
