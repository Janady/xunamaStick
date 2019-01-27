using UnityEngine;
using UnityEngine.UI;
using Libs.Resource;

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
        GameObjectManager.Destroy(gameObject);
    }
}
