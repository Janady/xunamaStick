using UnityEngine;
using Libs.Event;
using System.Collections;

public class Root : MonoBehaviour {
    private static Root Instance = null;

    private void Awake()
    {
        if (null != Instance)
        {
            Debug.LogError("Error, only once should be called!");
            return;
        }
        Screen.sleepTimeout = SleepTimeout.NeverSleep; // screen stay on
#if UNITY_EDITOR
        Application.runInBackground = true;
#endif
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        gameObject.AddComponent<Libs.Coroutine.CoroutineHandler>();
        gameObject.AddComponent<GameManager>();
        AppAudioModel.Instance().RunMusic(AppAudioName.BGM);
    }
    private void doit(string s)
    {
        Debug.Log(s);
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnEnable()
    {
        EventMgr.Instance.AddEvent(EventNameData.AdminOpr, AdminOprHandler);
    }
    private void OnDisable()
    {
        EventMgr.Instance.RemoveEvent(EventNameData.AdminOpr, AdminOprHandler);
    }
    void AdminOprHandler(object dispatcher, string eventName, object value)
    {
        Libs.Resource.UIManager.CloseUI();
        Libs.Resource.UIManager.OpenUI(Config.UI.UIPath.AdminPanel);
    }
}
