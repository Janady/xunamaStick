using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Libs.Resource;
using View;

namespace UI.Widget
{
    public class FileManager : MonoBehaviour
    {
        public string path="/";
        private DirectoryInfo _directory;
        public Action<string> callBack;
        public string[] reg;
        private Text title;
        private Transform _listTr;
        private Transform listTr
        {
            get
            {
                if(_listTr == null) _listTr = transform.FindChild("Content").FindChild("FilesList").FindChild("ScrollPanel");
                return _listTr;
            }
        }

        public static void openf(string path, Action<string> action, params string[] reg)
        {
            GameObject go = Libs.Resource.UIManager.OpenUI(Config.UI.UIPath.FileManager);
            FileManager fm = go.GetComponent<FileManager>();
            fm.path = path;
            fm.reg = reg;
            fm.callBack = action;
        }
        private void OnEnable()
        {
            DirectoryInfo direction = new DirectoryInfo(path);
            if (!direction.Exists)
            {
                direction = new DirectoryInfo("/");
            }
            Button pre = transform.FindChild("Content").FindChild("nav").FindChild("use").GetComponent<Button>();
            pre.onClick.AddListener(() =>
            {
                initFileList(_directory.Parent);
            });
            initFileList(direction);
        }
        private void initNav(DirectoryInfo directory)
        {
            _directory = directory;
            UIManager.CloseUI(listTr);
            if (title == null) title = transform.FindChild("Content").FindChild("nav").FindChild("title").GetComponent<Text>();
            transform.FindChild("Content").FindChild("nav").FindChild("use").gameObject.SetActive(directory.Parent != null);

        }
        private void initFileList(DirectoryInfo directory)
        {
            if (directory == null) return;
            initNav(directory);

            title.text = directory.Name;
            StartCoroutine(listRow(directory));
        }
        private IEnumerator listRow(DirectoryInfo directory)
        {
            int i = 0;
            foreach (string ptn in reg)
            {
                foreach (FileInfo fi in directory.GetFiles(ptn))
                {
                    setRow(fi, null, i++);
                    yield return new WaitForEndOfFrame();
                }
            }
            foreach (DirectoryInfo di in directory.GetDirectories())
            {
                setRow(null, di, i++);
                yield return new WaitForEndOfFrame();
            }
        }

        private void setRow(FileInfo fi, DirectoryInfo di, int index)
        {
            GameObject go = UIManager.OpenUI("FileRow", null, listTr, index);
            FileRowView cv = go.GetComponent<FileRowView>();
            cv.setValueAndCallback(fi, di, rowCallback);
        }
        private void rowCallback(FileInfo fi, DirectoryInfo di)
        {
            if (fi != null)
            {
                if (callBack != null) callBack(fi.FullName);
                GameObjectManager.Destroy(gameObject);
            }
            else if (di != null)
            {
                initFileList(di);
            }
        }
    }
}
