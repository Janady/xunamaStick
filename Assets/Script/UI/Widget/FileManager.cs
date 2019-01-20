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
        public string reg;
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

        public static void openf(string path, Action<string> action, string reg = "*")
        {
            GameObject go = Libs.Resource.UIManager.OpenUI(Config.UI.UIPath.FileManager);
            FileManager fm = go.GetComponent<FileManager>();
            fm.path = path;
            fm.reg = reg;
            fm.callBack = action;
        }
        private void Start()
        {
            DirectoryInfo direction = new DirectoryInfo(path);
            Button pre = transform.FindChild("Content").FindChild("nav").FindChild("use").GetComponent<Button>();
            pre.onClick.AddListener(()=>
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
            if (!directory.Exists)
            {
                return;
            }
            StartCoroutine(foldRow(directory));
            StartCoroutine(filesRow(directory));
        }
        private IEnumerator foldRow(DirectoryInfo directory)
        {

            foreach (DirectoryInfo di in directory.GetDirectories())
            {
                GameObject go = UIManager.OpenUI("FileRow", null, listTr);
                FileRowView cv = go.GetComponent<FileRowView>();
                cv.setValueAndCallback(null, di, rowCallback);
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator filesRow(DirectoryInfo directory)
        {

            foreach (FileInfo fi in directory.GetFiles(reg))
            {
                GameObject go = UIManager.OpenUI("FileRow", null, listTr);
                FileRowView cv = go.GetComponent<FileRowView>();
                cv.setValueAndCallback(fi,null, rowCallback);
                yield return new WaitForEndOfFrame();
            }
        }
        private void rowCallback(FileInfo fi, DirectoryInfo di)
        {
            if (fi != null)
            {
                if (callBack != null) callBack(fi.FullName);
                Destroy(gameObject);
            }
            else if (di != null)
            {
                initFileList(di);
            }
        }
    }
}
