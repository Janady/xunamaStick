using UnityEngine;
using Libs.Event;
using System.Collections;
using Libs.Resource;

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
        StartCoroutine(loading());
    }
    private IEnumerator loading()
    {
        Debug.Log(">>>>>>>>>>>>>> loading " + System.DateTime.Now.ToShortTimeString() + System.DateTime.Now.Millisecond + " start <<<<<<<<<<<<<<<<<<");
        GameObjectManager.InstantiatePrefabs("Bird", 0, true);
        yield return new WaitForEndOfFrame();
        for (int i=0; i<15; i++)
        {
            Libs.Resource.GameObjectManager.InstantiatePrefabs("pin", i, true);
        }
        yield return new WaitForEndOfFrame();
        GameObjectManager.InstantiatePrefabs("targetDiamondPink", 0, true);
        GameObjectManager.InstantiatePrefabs("targetDiamondGreen", 0, true);
        GameObjectManager.InstantiatePrefabs("targetDiamondBlue", 0, true);
        yield return new WaitForEndOfFrame();

        // loading music
        string APP_AUDIO_PATH = "Audio/";
        for (int i = 0; i < 3; i++)
        {
            ResourceManager.LoadResource(APP_AUDIO_PATH + AppAudioName.BGMRAND + i);
            yield return new WaitForEndOfFrame();
        }
        ResourceManager.LoadResource(APP_AUDIO_PATH + AppAudioName.Button1);
        yield return new WaitForEndOfFrame();
        ResourceManager.LoadResource(APP_AUDIO_PATH + AppAudioName.Button2);
        yield return new WaitForEndOfFrame();
        ResourceManager.LoadResource(APP_AUDIO_PATH + AppAudioName.Coin);
        yield return new WaitForEndOfFrame();
        ResourceManager.LoadResource(APP_AUDIO_PATH + AppAudioName.Gift);
        yield return new WaitForEndOfFrame();
        ResourceManager.LoadResource(APP_AUDIO_PATH + AppAudioName.Fail1);
        yield return new WaitForEndOfFrame();
        ResourceManager.LoadResource(APP_AUDIO_PATH + AppAudioName.Fail2);
        yield return new WaitForEndOfFrame();
        ResourceManager.LoadResource(APP_AUDIO_PATH + AppAudioName.Pass);
        yield return new WaitForEndOfFrame();
        ResourceManager.LoadResource(APP_AUDIO_PATH + AppAudioName.Shot);
        yield return new WaitForEndOfFrame();
        ResourceManager.LoadResource(APP_AUDIO_PATH + AppAudioName.Success);
        yield return new WaitForEndOfFrame();

        // loading ui
        GameObject go = UIManager.OpenUI(Config.UI.UIPath.HintPanel);
        go.SetActive(false);
        yield return new WaitForEndOfFrame();
        go = UIManager.OpenUI(Config.UI.UIPath.WinPanel);
        go.SetActive(false);
        yield return new WaitForEndOfFrame();
        go = UIManager.OpenUI(Config.UI.UIPath.LosePanel);
        go.SetActive(false);
        yield return new WaitForEndOfFrame();
        go = UIManager.OpenUI(Config.UI.UIPath.PayPanel);
        go.SetActive(false);
        yield return new WaitForEndOfFrame();
        go = UIManager.OpenUI(Config.UI.UIPath.ContanerSelectPanel);
        go.SetActive(false);
        yield return new WaitForEndOfFrame();
        UIManager.loadImage("Image/HeartPink", true);
        UIManager.loadImage("Image/HeartPink", true);
        UIManager.loadImage("Image/Gift", true);
        Debug.Log(">>>>>>>>>>>>>> loading " + System.DateTime.Now.ToString() + "-" + System.DateTime.Now.Millisecond + " done!!! <<<<<<<<<<<<<<<<<<");
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
