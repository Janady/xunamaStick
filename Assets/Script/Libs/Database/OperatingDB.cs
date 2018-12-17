
using System.Collections;
using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;
using Libs.Singleton;
using System.IO;

public class OperatingDB : MonoSingleton<OperatingDB>
{
    public DbAccess db;
    string appDBPath;

    /// <summary>
    /// 获取数据库引用
    /// </summary>
    public void CreateDataBase()
    {
        if (db != null)
            return;
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        appDBPath = Application.streamingAssetsPath + "Eam.db";

#elif UNITY_ANDROID || UNITY_IPHONE
        appDBPath = Application.persistentDataPath + "/ARPG.db";
		if(!File.Exists(appDBPath))
		{
			StartCoroutine(CopyDB());
		}
#endif
        db = new DbAccess("URI=file:" + appDBPath);
    }

    IEnumerator CopyDB()
    {
        string loadPath = string.Empty;
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
        loadPath = Application.streamingAssetsPath + "/Eam.db";
#elif UNITY_ANDROID
		loadPath = "jar:file://" + Application.dataPath + "!/assets" + "/Eam.db";
#elif UNITY_IPHONE
		loadPath = + Application.dataPath + "/Raw" + "/Eam.db";
#endif
        WWW www = new WWW(loadPath);
        yield return www;
        File.WriteAllBytes(appDBPath, www.bytes);
    }
}