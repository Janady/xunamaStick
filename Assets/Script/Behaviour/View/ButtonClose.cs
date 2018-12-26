using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonClose : MonoBehaviour
{
    public Button button;
    // Use this for initialization
    void Start()
    {
        EventTriggerListener.Get(button.gameObject).onClick = OnClose;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnClose(GameObject go)
    {
        Destroy(gameObject);
    }
}
