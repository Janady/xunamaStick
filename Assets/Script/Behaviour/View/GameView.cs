using UnityEngine;
using UnityEngine.UI;
using Libs.Event;
using Libs.Resource;

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

    private void Start()
    {
        remaining = transform.FindChild("Remaining");
        passSet = transform.FindChild("PassSet");
        actionSet = transform.FindChild("ActionSet");
        prepareSet = transform.FindChild("PrepareSet");
        prepareLips = prepareSet.gameObject.AddComponent<PrepareLipsView>();
        playBtn = actionSet.FindChild("Play").gameObject.GetComponent<Button>();
        EventTriggerListener.Get(playBtn.gameObject).onClick = OnPlayButtonClick;
        buyBtn = actionSet.FindChild("Buy").gameObject.GetComponent<Button>();
        EventTriggerListener.Get(buyBtn.gameObject).onClick = OnPlayButtonClick;
        tryBtn = actionSet.FindChild("Try").gameObject.GetComponent<Button>();
        EventTriggerListener.Get(tryBtn.gameObject).onClick = OnTryButtonClick;
    }

    private void passLevel(int level)
    {
        int passNum = passSet.childCount;
        for (int i=0; i<level && i< passNum; i++)
        {
            GameObject go = passSet.GetChild(i).GetChild(0).gameObject as GameObject;
            Text text = go.GetComponent<Text>();
            text.color = Color.red;
        }
        for (int i=level; i<passNum; i++)
        {
            GameObject go = passSet.GetChild(i).GetChild(0).gameObject as GameObject;
            // float width = go.GetComponent<RectTransform>().rect.width;
            Text text = go.GetComponent<Text>();
            text.color = Color.white;
        }
    }
    private void reset()
    {
        passSet.gameObject.SetActive(true);
        actionSet.gameObject.SetActive(true);
        UIManager.CloseUI(prepareSet);
        passLevel(0);
    }
    private void gameLevel(int level)
    {
        actionSet.gameObject.SetActive(false);
        passLevel(level);
        prepareLips.prepareLips(5);
    }
    #region registor event
    private void OnEnable()
    {
        EventMgr.Instance.AddEvent(EventNameData.GameLevel, OnGameLavel);
    }
    private void OnDisable()
    {
        EventMgr.Instance.RemoveEvent(EventNameData.GameLevel, OnGameLavel);
    }
#endregion
    #region game level event
    private void OnGameLavel(object dispatcher, string eventName, object value)
    {
        int level = 0;
        if (value != null) level = (int)value;
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
        EventMgr.Instance.DispatchEvent(EventNameData.ButtonStart, true);
    }
    private void OnPlayButtonClick(GameObject go)
    {
        EventMgr.Instance.DispatchEvent(EventNameData.ButtonStart, false);
    }
#endregion
}