using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UI.Widget;
using System.Collections;

public class NavView : MonoBehaviour
{
    private Text titleText;
    private Button restartBtn;
    private Button unlockingBtn;
    private Button replenishBtn;
    private Button backBtn;
    private OnButtonCallBack _btn1Callback;
    private OnButtonCallBack _btn2Callback;
    // Use this for initialization
    void Start()
    {
        restartBtn = transform.FindChild("restart").gameObject.GetComponent<Button>();
        EventTriggerListener.Get(restartBtn.gameObject).onClick = OnRestartButtonClick;
        unlockingBtn = transform.FindChild("unlocking").gameObject.GetComponent<Button>();
        EventTriggerListener.Get(unlockingBtn.gameObject).onClick = OnUnlockingButtonClick;
        replenishBtn = transform.FindChild("replenish").gameObject.GetComponent<Button>();
        EventTriggerListener.Get(replenishBtn.gameObject).onClick = OnReplenishButtonClick;
        backBtn = transform.FindChild("back").gameObject.GetComponent<Button>();
        EventTriggerListener.Get(backBtn.gameObject).onClick = OnBackButtonClick;
    }

    public string Title
    {
        set
        {
            if (titleText == null) titleText = transform.FindChild("title").GetComponent<Text>();
            titleText.text = value;
        }
    }
    public void setBtn1(string name, OnButtonCallBack callBack) {
        _btn1Callback = callBack;
        Text text = unlockingBtn.transform.FindChild("Text").GetComponent<Text>();
        text.text = name;
    }
    public void setBtn2(string name, OnButtonCallBack callBack)
    {
        _btn2Callback = callBack;
        Text text = replenishBtn.transform.FindChild("Text").GetComponent<Text>();
        text.text = name;
    }
    #region button click event
    private void OnUnlockingButtonClick(GameObject go)
    {
        if (_btn1Callback != null) _btn1Callback();
    }
    private void OnRestartButtonClick(GameObject go)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnReplenishButtonClick(GameObject go)
    {
        if (_btn2Callback != null) _btn2Callback();
    }
    private void OnBackButtonClick(GameObject go)
    {
        Destroy(transform.parent.gameObject);
    }
    #endregion
}
