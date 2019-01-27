using UnityEngine;
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using Libs.Resource;

public class VedioListView : MonoBehaviour
{
    private Dictionary<FileInfo, bool> dict;
    private Action callBack;
    private Transform tr;
    // Use this for initialization
    void Start()
    {
        dict = new Dictionary<FileInfo, bool>();
        loadVedio(Config.Constant.UsbPath, false);
        loadVedio(Config.Constant.VedioPath, true);

        tr = transform.FindChild("ScrollPanel");
        StartCoroutine(init(tr));
    }
    private void loadVedio(string path, bool local)
    {
        foreach(FileInfo fi in FileUtil.getVedios(path))
        {
            dict.Add(fi, local);
        }
    }
    IEnumerator init(Transform tr)
    {
        int i = 0;
        foreach (var item in dict)
        {
            GameObject go = UIManager.OpenUI("vedioRow", null, tr, i++);
            VedioRowView vrv = go.GetComponent<VedioRowView>();
            vrv.file = item.Key;
            if (item.Value)
            {
                vrv.SetCallBack(delete, VedioRowView.ActionType.Delete);
            }
            else
            {
                vrv.SetCallBack(move, VedioRowView.ActionType.Add);
            }
            yield return new WaitForEndOfFrame();
        }
    }
    void delete(FileInfo fileInfo)
    {
        fileInfo.Delete();
        Debug.Log(fileInfo.FullName);
    }
    void move(FileInfo fileInfo)
    {
        string name = Config.Constant.VedioPath + "/" + fileInfo.Name;
        fileInfo.CopyTo(name);
    }
}
