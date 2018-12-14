using UnityEngine;
using System.Collections;
using Libs.Event;
using Libs.Resource;

public class GameManager : MonoBehaviour {
    private bool isGameOver = false;
    private GameLevel gameLevel;
    // Use this for initialization
    private GameView gameView;
	void Start () {
        GameObject go = UIManager.OpenUI(Config.UI.UIPath.GamePanel);
        gameView = go.AddComponent<GameView>();
        gameView.setupCallback(tryGame, playGame, buy);
    }
	
	// Update is called once per frame
    private void OnEnable()
    {
        EventMgr.Instance.AddEvent(EventNameData.GamePass, OnGamePass);
    }
    private void OnDisable()
    {
        EventMgr.Instance.RemoveEvent(EventNameData.GamePass, OnGamePass);
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
    private void OnGamePass(object dispatcher, string eventName, object value)
    {
        gameView.stopGame();
        bool pass = true;
        if (value != null) pass = (bool)value;
        clear();
        if (pass)
        {
            nextLevel();
        }
        else
        {
            failed();
        }
    }
    private void clear()
    {
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Pin"))
        {
            Destroy(go);
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Rotator"))
        {
            Destroy(go);
        }
    }

    private void startGame(bool trial)
    {
        gameLevel = new GameLevel(trial);
        nextLevel();
    }
    private void nextLevel()
    {
        if (!gameLevel.next())
        {
            passAll();
            return;
        }
        GameFacts fact = new GameFacts(gameLevel.level(), 3 + gameLevel.level()*2);
        // EventMgr.Instance.DispatchEvent(EventNameData.GameFacts, fact);
        UI.Widget.CommonTips.OpenTips(UI.Widget.TipsType.AUTO_CLOSE, gameLevel.ToString(), null, null, () => {
            //ResourceManager.InstantiatePrefab("pinSpawn");
            GameObject go = ResourceManager.InstantiatePrefab("target");
            Rotator target = go.AddComponent<Rotator>();
            target.Total = fact.Count;
            gameView.startGame(fact.Level, fact.Count);
        }, 1);
    }
    private void passAll()
    {
        string effect = "starEffect";
        EffectManager.LoadEffect(effect);
        UI.Widget.CommonTips.OpenTips(UI.Widget.TipsType.AUTO_CLOSE, "挑战成功！", null, null, () => {
            EffectManager.ReleaseEffect(effect);
        }, 1);
    }
    private void failed()
    {
        UI.Widget.CommonTips.OpenTips(UI.Widget.TipsType.AUTO_CLOSE, "挑战失败！", null, null, () => {
                
            }, 1);
    }
    #region view callback
    private void playGame()
    {
        startGame(false);
    }
    private void tryGame()
    {
        startGame(true);
    }

    private void buy()
    {

    }
#endregion
}
