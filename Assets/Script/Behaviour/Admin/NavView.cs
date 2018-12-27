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
    private OnButtonCallBack _backCallback;
    private OnButtonCallBack _restartCallback;
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
    public OnButtonCallBack BackCallback
    {
        get {
            return _backCallback;
        }
        set
        {
            _backCallback = value;
        }
    }
    public OnButtonCallBack RestartCallback
    {
        get
        {
            return _restartCallback;
        }
        set
        {
            _restartCallback = value;
        }
    }
    #region button click event
    private void OnUnlockingButtonClick(GameObject go)
    {
    }
    private void OnRestartButtonClick(GameObject go)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    private void OnReplenishButtonClick(GameObject go)
    {
    }
    private void OnBackButtonClick(GameObject go)
    {
        Destroy(transform.parent.gameObject);
    }
    #endregion
}
