using UnityEngine;
using UnityEngine.UI;
using Libs.Event;
using Libs.Resource;
using UI.Widget;
using DG.Tweening;

public class GameView : MonoBehaviour
{
    private Transform actionSet;
    private Button playBtn;
    private Button buyBtn;
    private Button tryBtn;
    private Text remainingText;
    private Transform passSet;
    private Transform prepareSet;
    private PrepareLipsView prepareLips;
    private OnButtonCallBack tryCallback;
    private OnButtonCallBack playCallback;
    private OnButtonCallBack buyCallback;
    private GameObject girl;

    private void Start()
    {
        remainingText = transform.FindChild("Remaining").FindChild("remain").GetComponent<Text>();
        Coin.GetInstance().GameTimeCallback(x=> {
            remainingText.text = x.ToString();
        });
        passSet = transform.FindChild("PassSet");
        actionSet = transform.FindChild("ActionSet");
        initPass();
        prepareLips = prepareSet.gameObject.GetComponent<PrepareLipsView>();
        
        girl = transform.FindChild("Girl").gameObject;
        playBtn = actionSet.FindChild("Play").gameObject.GetComponent<Button>();
        EventTriggerListener.Get(playBtn.gameObject).onClick = OnPlayButtonClick;
        buyBtn = actionSet.FindChild("Buy").gameObject.GetComponent<Button>();
        EventTriggerListener.Get(buyBtn.gameObject).onClick = OnBuyButtonClick;
        tryBtn = actionSet.FindChild("Try").gameObject.GetComponent<Button>();
        EventTriggerListener.Get(tryBtn.gameObject).onClick = OnTryButtonClick;

        zoom(actionSet.FindChild("Playbg"), true);
        zoom(actionSet.FindChild("Buybg"), true);
        zoom(actionSet.FindChild("Trybg"), true);
    }
    private void zoom(Transform tr, bool big)
    {
        float ratio = big ? 1.2f : 0.9f;
        Vector3 vector = new Vector3(ratio, ratio, 1f);
        tr.DOScale(vector, 1).SetEase(Ease.Linear).OnComplete(()=> {
            zoom(tr, !big);
        });
    }
    private void initPass()
    {
        prepareSet = transform.FindChild("PrepareSet");
        int passNum = passSet.childCount;
        for (int i = 0; i < passNum; i++)
        {
            GameObject go = passSet.GetChild(i).gameObject as GameObject;
            Image img = go.GetComponent<Image>();

            img.sprite = UIManager.GenSprite(UIManager.loadImage("Image/HeartPink", true));
            GameObject l = passSet.GetChild(i).FindChild("lock").gameObject;
            l.SetActive(false);
        }
    }

    public void setupCallback(OnButtonCallBack tryCallback, OnButtonCallBack playCallback, OnButtonCallBack buyCallback)
    {
        this.tryCallback = tryCallback;
        this.buyCallback = buyCallback;
        this.playCallback = playCallback;
    }

    private void passLevel(GameLevel level)
    {
        int passNum = passSet.childCount;
        for (int i=0; i<level.level && i< passNum; i++)
        {
            GameObject go = passSet.GetChild(i).gameObject as GameObject;
            Image img = go.GetComponent<Image>();
            img.sprite = UIManager.GenSprite(UIManager.loadImage("Image/HeartPink", true));

            GameObject l = passSet.GetChild(i).FindChild("lock").gameObject;
            l.SetActive(false);
        }
        for (int i=level.level; i<passNum; i++)
        {
            GameObject go = passSet.GetChild(i).gameObject as GameObject;
            Image img = go.GetComponent<Image>();
            img.sprite = UIManager.GenSprite(UIManager.loadImage("Image/HeartGrey", true));

            GameObject l = passSet.GetChild(i).FindChild("lock").gameObject;
            l.SetActive(false);
        }
        for (int i = level.totalLevel; i < passNum; i++)
        {
            GameObject go = passSet.GetChild(i).FindChild("lock").gameObject;
            go.SetActive(true);
        }
    }
    private void reset()
    {
        passSet.gameObject.SetActive(true);
        actionSet.gameObject.SetActive(true);
        UIManager.CloseUI(prepareSet);
        initPass();
    }
    private void gameLevel(GameLevel level)
    {
        actionSet.gameObject.SetActive(false);
        passLevel(level);
    }

    public void startGame(GameLevel level, int lipsCount)
    {
        passLevel(level);
        prepareLips.prepareLips(lipsCount);
        actionSet.gameObject.SetActive(false);
        startCountdown(30);
        girl.SetActive(false);
    }
    public void stopGame()
    {
        initPass();
        prepareLips.prepareLips(0);
        actionSet.gameObject.SetActive(true);
        cancelCountdown();
        girl.SetActive(true);
    }
    private void startCountdown(int seconds)
    {
        transform.FindChild("Countdown").gameObject.SetActive(true);
        Text txt = transform.FindChild("Countdown").GetChild(0).GetComponent<Text>();
        UI.Widget.CountDown.countDown(seconds, txt, ()=> {
            EventMgr.Instance.DispatchEvent(EventNameData.GamePass, false);
        });
    }
    private void cancelCountdown()
    {
        transform.FindChild("Countdown").gameObject.SetActive(false);
        Text txt = transform.FindChild("Countdown").GetChild(0).GetComponent<Text>();
        UI.Widget.CountDown.cancel(txt);
    }

    #region button click event
    private void OnTryButtonClick(GameObject go)
    {
        if (tryCallback != null) tryCallback();
        // EventMgr.Instance.DispatchEvent(EventNameData.ButtonStart, true);
    }
    private void OnPlayButtonClick(GameObject go)
    {
        if (playCallback != null) playCallback();
        // EventMgr.Instance.DispatchEvent(EventNameData.ButtonStart, false);
    }
    private void OnBuyButtonClick(GameObject go)
    {
        if (buyCallback != null) buyCallback();
        // EventMgr.Instance.DispatchEvent(EventNameData.ButtonStart, false);
    }
    #endregion
}