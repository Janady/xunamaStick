using UnityEngine;
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
    }
	
	// Update is called once per frame
	void Update () {
	}
}
