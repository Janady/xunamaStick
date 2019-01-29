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
    private void OnEnable()
    {
        Refresh();
    }
    void Start()
    {
        tr = transform.FindChild("ScrollPanel");
        StartCoroutine(init());
    }
    
    IEnumerator init()
    {
        if (tr == null) yield break;
        int i = 0;
        foreach (FileInfo fi in FileUtil.getVedios(Config.Constant.VedioPath))
        {
            GameObject go = UIManager.OpenUI("vedioRow", null, tr, i++);
            VedioRowView vrv = go.GetComponent<VedioRowView>();
            vrv.SetCallBack(delete, VedioRowView.ActionType.Delete);
            vrv.file = fi;
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
    public void Refresh()
    {
        if (tr == null) return;
        UIManager.CloseUI(tr);
        StartCoroutine(init());
    }
}
