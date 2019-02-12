using UnityEngine;
using System.Collections.Generic;
using Libs.Event;
using Libs.Resource;
using DG.Tweening;
using Mod;

public class GameManager : MonoBehaviour {
    private GameLevel gameLevel;
    // Use this for initialization
    private GameView gameView;
    private GameObject bird;
#if !UNITY_EDITOR
    private MovieView movie;
#endif
    private const int MaxWaiting = 1000;
    private int waitingCount = 0;
    private enum STATUS
    {
        Running,
        Idle,
        Show,
        Prepare,
        Waiting
    }
    private STATUS status = STATUS.Idle;
    void Start () {
        GameObject go = UIManager.OpenUI(Config.UI.UIPath.GamePanel);
        gameView = go.AddComponent<GameView>();
        gameView.setupCallback(tryGame, playGame, buy);
#if !UNITY_EDITOR
        GameObject vedioObject = UIManager.OpenUI(Config.UI.UIPath.MoviePanel);
        movie = vedioObject.GetComponent<MovieView>();
#endif
        bird = GameObjectManager.InstantiatePrefabs("Bird");
    }
    private void birdMovement(System.Action action)
    {
        bird.SetActive(true);
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
                bird.SetActive(false);
                action();
            });
        });
    }
    // Update is called once per frame
    private void OnEnable()
    {
        EventMgr.Instance.AddEvent(EventNameData.GamePass, OnGamePass);
    }
    private void OnDisable()
    {
        EventMgr.Instance.RemoveEvent(EventNameData.GamePass, OnGamePass);
        status = STATUS.Idle;
    }
    private int vedioCount = 0;
    private const int vedioPlay = 3000;
    private void Update()
    {
        if (vedioCount >= 0) vedioCount++;
        if (Input.GetButtonDown("Fire1")) {
            vedioCount = 0;
            // Debug.Log("vedio stop" + System.DateTime.Now.ToString() + " " + System.DateTime.Now.Millisecond);
#if !UNITY_EDITOR
            movie.stop();
#endif
        }
        if (vedioCount > vedioPlay)
        {
            // Debug.Log("vedio on" + System.DateTime.Now.ToString() + " " + System.DateTime.Now.Millisecond);
            vedioCount = -1;
#if !UNITY_EDITOR
            movie.play();
#endif
        }
        /*
        switch (status)
        {
            case STATUS.Running:
                break;
            case STATUS.Idle:
                status = STATUS.Show;
                birdMovement(() => {
#if !UNITY_EDITOR
                    if (status == STATUS.Show) movie.play();
#endif
                });
                break;
            case STATUS.Show:
                if (Input.GetButtonDown("Fire1"))
                {
                    status = STATUS.Waiting;
                    waitingCount = 0;
                    bird.SetActive(false);
#if !UNITY_EDITOR
                    movie.stop();
#endif
                }
                break;
            case STATUS.Prepare:
                status = STATUS.Running;
                bird.SetActive(false);
#if !UNITY_EDITOR
                movie.stop();
#endif
                break;
            case STATUS.Waiting:
                if (waitingCount++ > MaxWaiting)
                {
                    status = STATUS.Idle;
                }
                break;
        }
        */
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
            AppAudioModel.Instance().RunAudio(AppAudioName.Pass);
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
            go.transform.position = new Vector3(-20,-20,-20);
            GameObjectManager.Destroy(go);
        }
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Rotator"))
        {
            GameObjectManager.Destroy(go);
        }
    }
    private int cabinetId;
    private void startGame(bool trial)
    {
        status = STATUS.Prepare;
        gameLevel = new GameLevel(trial);
        nextLevel();
    }
    private void nextLevel()
    {
        if (gameLevel.pass())
        {
            passAll();
            AppAudioModel.Instance().RunMusic(AppAudioName.BGM);
            return;
        }
        AppAudioModel.Instance().RunMusic(AppAudioName.BGMRAND + gameLevel.level);
        GameFacts fact = new GameFacts(gameLevel.level, cabinetId);
        string targetStr = null;
        switch (Random.Range(0, 3))
        {
            case 0:
                targetStr = "targetDiamondPink";
                break;
            case 1:
                targetStr = "targetDiamondGreen";
                break;
            case 2:
                targetStr = "targetDiamondBlue";
                break;
        }
        GameObject go = GameObjectManager.InstantiatePrefabs(targetStr);
        go.transform.position = new Vector3(0, 4, 0);
        Rotator target = go.GetComponent<Rotator>();
        if (target == null)
            target = go.AddComponent<Rotator>();
        target.Fact = fact;
        gameView.startGame(gameLevel, fact.Count);
        showHint();
        // EventMgr.Instance.DispatchEvent(EventNameData.GameFacts, fact);
        // UI.Widget.CommonTips.OpenTips(UI.Widget.TipsType.AUTO_CLOSE, gameLevel.ToString(), null, null, () => {
        //ResourceManager.InstantiatePrefab("pinSpawn");
        // }, 1);
        gameLevel.next();
    }
    private void showHint()
    {
        GameObject go = UIManager.OpenUI(Config.UI.UIPath.HintPanel);
        Title title = go.GetComponent<Title>();
        title.title = gameLevel.ToString();
        title.description = gameLevel.description();
    }
    private void passAll()
    {
        //status = STATUS.Idle;
        AppAudioModel.Instance().RunAudio(AppAudioName.Success);
        showSuccess();
        settle(true);
    }
    private void settle(bool doGame)
    {
        Cabinet cabinet = Cabinet.GetById(cabinetId);
        if (cabinet != null)
        {
            Service.LockingPlateService.Instance().openLock(cabinet, Service.LockingPlateService.OpenType.Sold);
            if (!doGame) return;
            Goods good = cabinet.Good();
            if (good == null) return;
            Game g = Game.get();
            g.offset = good.Price / g.price - g.lucky;
            g.lucky = 0;
            g.update();
            Debug.Log(g);
        }
    }
    private void showSuccess()
    {
        UIManager.OpenUI(Config.UI.UIPath.WinPanel);
    }
    private void failed()
    {
        UIManager.OpenUI(Config.UI.UIPath.LosePanel);
        AppAudioModel.Instance().RunAudio(Random.Range(0, 1f) > 0.5f ? AppAudioName.Fail1 : AppAudioName.Fail2);
        status = STATUS.Idle;
        AppAudioModel.Instance().RunMusic(AppAudioName.BGM);
    }
#region view callback
    private void playGame()
    {
        // startGame(false);
        if (Coin.GetInstance().afford() > 0)
        {
            GameObject gl = UIManager.OpenUI(Config.UI.UIPath.ContanerSelectPanel);
            ContanerSelectView list = gl.GetComponent<ContanerSelectView>();
            list.setCallback((id) => {
                AppAudioModel.Instance().RunAudio(AppAudioName.Gift);
                Mod.Cabinet cabinet = Mod.Cabinet.GetById(id);
                if (cabinet == null || cabinet.Good() == null) return;
                if (!cabinet.Enabled || cabinet.Count <= 0) return;
                GameObjectManager.Destroy(gl);
                Coin.GetInstance().consume(id);
                cabinetId = id;
                startGame(false);
                Game g = Game.get();
                g.lucky = g.lucky + 1;
                g.update();
            });
            return;
        }
        GameObject go = UIManager.OpenUI(Config.UI.UIPath.PayPanel);
        PayView pv = go.GetComponent<PayView>();
        pv.amount = 4;
    }
    private void tryGame()
    {
        startGame(true);
    }

    private void buy()
    {
        GameObject go = UIManager.OpenUI(Config.UI.UIPath.ContanerSelectPanel);
        ContanerSelectView list = go.GetComponent<ContanerSelectView>();
        list.setCallback((id)=> {
            AppAudioModel.Instance().RunAudio(AppAudioName.Gift);
            Mod.Cabinet cabinet = Mod.Cabinet.GetById(id);
            if (cabinet == null || cabinet.Good() == null) return;
            if (!cabinet.Enabled || cabinet.Count <= 0) return;
            if (Coin.GetInstance().afford((uint)cabinet.Good().Price) > 0) {
                Coin.GetInstance().consume(id, false, (uint)cabinet.Good().Price);
                showSuccess();
                settle(false);
            }
            else
            {
                GameObject pay = UIManager.OpenUI(Config.UI.UIPath.PayPanel);
                PayView pv = pay.GetComponent<PayView>();
                pv.amount = cabinet.Good().Price;
            }
        });
    }
#endregion
}
