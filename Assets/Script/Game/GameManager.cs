using UnityEngine;
using System.Collections;
using Libs.Event;
using Libs.Resource;

public class GameManager : MonoBehaviour {
    private bool isGameOver = false;
    private GameLevel gameLevel;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
    private void OnEnable()
    {
        EventMgr.Instance.AddEvent(EventNameData.ButtonStart, OnGameStart);
    }
    private void OnDisable()
    {
        EventMgr.Instance.RemoveEvent(EventNameData.ButtonStart, OnGameStart);
    }
    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            Debug.Log("GameOver!");
            foreach (GameObject go in GameObject.FindGameObjectsWithTag("Pin"))
            {
                Destroy(go);
            }
        }
    }

    /*
     * event handler
     */
    private void OnGameStart(object dispatcher, string eventName, object value)
    {
        bool trial = true;
        if (value != null) trial = (bool)value;
        gameLevel = new GameLevel(trial);
        startGame();
    }

    private void startGame()
    {
        gameLevel.next();
        EventMgr.Instance.DispatchEvent(EventNameData.GameLevel, gameLevel.level());
        UI.Widget.CommonTips.OpenTips(UI.Widget.TipsType.AUTO_CLOSE, gameLevel.ToString(), null, null, () => {
            //ResourceManager.InstantiatePrefab("pinSpawn");
            ResourceManager.InstantiatePrefab("target");
        }, 1);
    }
}
