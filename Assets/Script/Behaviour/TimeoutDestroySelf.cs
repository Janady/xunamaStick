using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class TimeoutDestroySelf : MonoBehaviour
{
    public int timeout;
    public bool doFade = true;
    public Text text;
    // Use this for initialization
    void Start()
    {
    }
    private void OnEnable()
    {
        UI.Widget.CountDown.countDown(timeout, text, () => {
            Libs.Resource.GameObjectManager.Destroy(gameObject);
        });
        if (doFade)
        {
            Image image = GetComponent<Image>();
            image.CrossFadeAlpha(0.3f, timeout, true);
            //image.DOColor(new Color(255, 255, 255, 0.5f), (float)timeout).SetEase(Ease.Linear);
        }
    }
    private void OnDisable()
    {
        UI.Widget.CountDown.cancel(text);
    }
    private void OnDestroy()
    {
        UI.Widget.CountDown.cancel(text);
    }
}
