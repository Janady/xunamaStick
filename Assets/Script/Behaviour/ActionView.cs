using UnityEngine;
using Libs.Event;
using UnityEngine.UI;

public class ActionView : MonoBehaviour {
    private Button start_button;
    // Use this for initialization
	void Start () {
        start_button = transform.FindChild("Play").gameObject.GetComponent<Button>();
        EventTriggerListener.Get(start_button.gameObject).onClick = OnStartButtonClick;
    }
    private void OnEnable()
    {
        EventMgr.Instance.AddEvent(EventNameData.GameStatus, OnGameStatus);
    }
    private void OnDisable()
    {
        EventMgr.Instance.RemoveEvent(EventNameData.GameStatus, OnGameStatus);
    }
    // Update is called once per frame
    void Update () {
	
	}
    
    private void OnStartButtonClick(GameObject go)
    {
        EventMgr.Instance.DispatchEvent(EventNameData.ButtonTry);
    }

    private void OnGameStatus(object dispatcher, string eventName, object value)
    {
        if (value == null) return;
        bool status = (bool)value;
        gameObject.SetActive(status);
    }
}
