using UnityEngine;
using System.Collections;

public class TimeoutDestroySelf : MonoBehaviour
{
    public int timeout;
    // Use this for initialization
    void Start()
    {
        UI.Widget.CountDown.countDown(timeout, null, () => {
            Destroy(gameObject);
        });
    }
    private void OnDestroy()
    {
        UI.Widget.CountDown.cancel();
    }
}
