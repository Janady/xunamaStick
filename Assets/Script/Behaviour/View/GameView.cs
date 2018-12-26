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
    private Transform remaining;
    private Transform passSet;
    private Transform prepareSet;
    private PrepareLipsView prepareLips;
    private OnButtonCallBack tryCallback;
    private OnButtonCallBack playCallback;
    private OnButtonCallBack buyCallback;
    private GameObject girl;

    private void Start()
    {
        remaining = transform.FindChild("Remaining");
        remaining.gameObject.SetActive(true);
        passSet = transform.FindChild("PassSet");
        actionSet = transform.FindChild("ActionSet");
        initPass();
        prepareLips = prepareSet.gameObject.GetComponent<PrepareLipsView>();
        actionSet.FindChild("Play").DOPunchScale(new Vector3(.9f, .9f, 1f), 5f, 1).SetEase(Ease.Linear).SetLoops(-1, LoopType.Restart);
        girl = transform.FindChild("Girl").gameObject;
        playBtn = actionSet.FindChild("Play").gameObject.GetComponent<Button>();
        EventTriggerListener.Get(playBtn.gameObject).onClick = OnPlayButtonClick;
        buyBtn = actionSet.FindChild("Buy").gameObject.GetComponent<Button>();
        EventTriggerListener.Get(buyBtn.gameObject).onClick = OnBuyButtonClick;
        tryBtn = actionSet.FindChild("Try").gameObject.GetComponent<Button>();
        EventTriggerListener.Get(tryBtn.gameObject).onClick = OnTryButtonClick;
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
        }
    }

    public void setupCallback(OnButtonCallBack tryCallback, OnButtonCallBack playCallback, OnButtonCallBack buyCallback)
    {
        this.tryCallback = tryCallback;
        this.buyCallback = buyCallback;
        this.playCallback = playCallback;
    }

    private void passLevel(int level)
    {
        int passNum = passSet.childCount;
        for (int i=0; i<level && i< passNum; i++)
        {
            GameObject go = passSet.GetChild(i).gameObject as GameObject;
            Image img = go.GetComponent<Image>();

            img.sprite = UIManager.GenSprite(UIManager.loadImage("Image/HeartPink", true));
        }
        for (int i=level; i<passNum; i++)
        {
            GameObject go = passSet.GetChild(i).gameObject as GameObject;
            Image img = go.GetComponent<Image>();
            img.sprite = UIManager.GenSprite(UIManager.loadImage("Image/HeartGrey", true));
            //img.sprite = UIManager.GenSprite(UIManager.loadImage("Image/HeartPink", true));
            // float width = go.GetComponent<RectTransform>().rect.width;
        }
    }
    private void reset()
    {
        passSet.gameObject.SetActive(true);
        actionSet.gameObject.SetActive(true);
        UIManager.CloseUI(prepareSet);
        initPass();
    }
    private void gameLevel(int level)
    {
        actionSet.gameObject.SetActive(false);
        passLevel(level);
    }

    public void startGame(int level, int lipsCount)
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
    #region registor event
    private void OnEnable()
    {
        EventMgr.Instance.AddEvent(EventNameData.GameFacts, OnGameFacts);
    }
    private void OnDisable()
    {
        EventMgr.Instance.RemoveEvent(EventNameData.GameFacts, OnGameFacts);
    }
#endregion
    #region game level event
    private void OnGameFacts(object dispatcher, string eventName, object value)
    {
        Debug.Log("OnGameFacts-gameview");
        if (value == null || !(value is GameFacts)) return;
        GameFacts fact = value as GameFacts;
        prepareLips.prepareLips(fact.Count);
        int level = fact.Level;
        switch (level)
        {
            case 0:
                reset();
                break;
            default:
                gameLevel(level);
                break;
        }
    }
    #endregion

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