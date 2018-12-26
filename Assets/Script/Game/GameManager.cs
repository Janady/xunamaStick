﻿using UnityEngine;
using System.Collections.Generic;
using Libs.Event;
using Libs.Resource;
using DG.Tweening;

public class GameManager : MonoBehaviour {
    private bool isGameOver = false;
    private GameLevel gameLevel;
    // Use this for initialization
    private GameView gameView;
    private GameObject bird;

    void Start () {
        GameObject go = UIManager.OpenUI(Config.UI.UIPath.GamePanel);
        gameView = go.AddComponent<GameView>();
        gameView.setupCallback(tryGame, playGame, buy);
    }
    private void birdMovement()
    {
        if (bird == null) return;
        List<Vector3> list = new List<Vector3>();
        bool normal = bird.transform.position.x > 0;
        for (int i = 8; i > -8; i -= 2)
        {
            list.Add(new Vector3(i, Random.Range(0f, 4f), 0));
        }
        if (!normal)
        {
            list.Reverse();
        }
        bird.transform.DOLocalPath(list.ToArray(), 15).SetEase(Ease.Linear).OnComplete(()=> {
            bird.transform.DOLocalRotate(new Vector3(0, normal ? 180 : 0, 0), 1f).OnComplete(()=> {
                birdMovement();
            });
        });
    }
    // Update is called once per frame
    private void OnEnable()
    {
        bird = ResourceManager.InstantiatePrefab("Bird");
        birdMovement();
        EventMgr.Instance.AddEvent(EventNameData.GamePass, OnGamePass);
    }
    private void OnDisable()
    {
        Destroy(bird);
        EventMgr.Instance.RemoveEvent(EventNameData.GamePass, OnGamePass);
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
        GameFacts fact = new GameFacts(gameLevel.level, 3 + gameLevel.level*2);
        string targetStr = Random.Range(-1f, 1f) < 0 ? "target" : "targetStar";
        GameObject go = ResourceManager.InstantiatePrefab(targetStr);
        Rotator target = go.AddComponent<Rotator>();
        target.Total = fact.Count;
        gameView.startGame(gameLevel, fact.Count);
        showHint();
        // EventMgr.Instance.DispatchEvent(EventNameData.GameFacts, fact);
        // UI.Widget.CommonTips.OpenTips(UI.Widget.TipsType.AUTO_CLOSE, gameLevel.ToString(), null, null, () => {
            //ResourceManager.InstantiatePrefab("pinSpawn");
        // }, 1);
    }
    private void showHint()
    {
        GameObject go = UIManager.OpenUI(Config.UI.UIPath.HintPanel);
        Title title = go.GetComponent<Title>();
        title.title = gameLevel.ToString();
    }
    private void passAll()
    {
        string effect = "starEffect";
        EffectManager.LoadEffect(effect);
        UIManager.OpenUI(Config.UI.UIPath.WinPanel);
    }
    private void failed()
    {
        UIManager.OpenUI(Config.UI.UIPath.LosePanel);
    }
    #region view callback
    private void playGame()
    {
        // startGame(false);
        GameObject go = UIManager.OpenUI(Config.UI.UIPath.PayPanel);
        PayView pv = go.GetComponent<PayView>();
        pv.amount = 4;
        pv.qrcodeId = "60014000";
    }
    private void tryGame()
    {
        startGame(true);
    }

    private void buy()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("check");
    }
#endregion
}
