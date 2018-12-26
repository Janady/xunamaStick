using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeoutDestroySelf : MonoBehaviour
{
    public int timeout;
    public Text text; 
    // Use this for initialization
    void Start()
    {
        UI.Widget.CountDown.countDown(timeout, text, () => {
            Destroy(gameObject);
        });
    }
    private void OnDestroy()
    {
        UI.Widget.CountDown.cancel();
    }
}
