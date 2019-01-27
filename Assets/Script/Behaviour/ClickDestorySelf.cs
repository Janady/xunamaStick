using UnityEngine;
using System.Collections;

public class ClickDestorySelf : MonoBehaviour
{
    public GameObject close;
    // Use this for initialization
    void Start()
    {
        if (close != null) EventTriggerListener.Get(close).onClick = OnClick;
    }
    private void OnClick(GameObject go)
    {
        Libs.Resource.GameObjectManager.Destroy(gameObject);
    }
}
