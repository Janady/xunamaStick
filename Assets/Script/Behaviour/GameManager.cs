using UnityEngine;
using System.Collections;
using Libs.Event;
using Libs.Resource;

public class GameManager : MonoBehaviour {
    public enum GameStatus
    {
        GameStart = 0,
        GamePass,
        GameOver
    }
    private bool isGameOver = false;
	// Use this for initialization
	void Start () {
        UIManager.OpenUI(Config.UI.UIPath.ActionPanel);
    }
	
	// Update is called once per frame
    private void OnEnable()
    {
        EventMgr.Instance.AddEvent(EventNameData.ButtonTry, OnGameStatus);
    }
    private void OnDisable()
    {
        EventMgr.Instance.RemoveEvent(EventNameData.ButtonTry, OnGameStatus);
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
    private void OnGameStatus(object dispatcher, string eventName, object value)
    {
        if (value == null) return;
        GameStatus gs = (GameStatus)value;
        switch (gs)
        {
            case GameStatus.GameStart:
                startGame();
                break;
            case GameStatus.GamePass:
                break;
            case GameStatus.GameOver:
                break;
        }
    }

    private void startGame()
    {
        UI.Widget.CommonTips.OpenTips(UI.Widget.TipsType.AUTO_CLOSE, "第一关", ()=> {
            ResourceManager.InstantiatePrefab("pinSpawn");
            ResourceManager.InstantiatePrefab("target");
        });
    }
}
