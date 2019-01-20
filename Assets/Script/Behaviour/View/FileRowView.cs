using System;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

namespace View
{
    public class FileRowView : MonoBehaviour
    {
        private Text titleText;
        private GameObject hintImage;
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
        private GameObject HintImage
        {
            get
            {
                if (hintImage == null)
                {
                    hintImage = transform.FindChild("Image").gameObject;
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
                HintImage.SetActive(true);
            }
            else if (di != null)
            {
                TitleText.text = di.Name;
                HintImage.SetActive(false);
            }
            fileInfo = fi;
            directoryInfo = di;
        }
    }
}
