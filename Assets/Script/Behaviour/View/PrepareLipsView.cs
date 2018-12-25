using UnityEngine;
using Libs.Resource;
using UnityEngine.UI;
using Libs.Event;

public class PrepareLipsView : MonoBehaviour
{
    private int lips = 0;
    private GameObject text;
    private void Start()
    {
    }
    public void prepareLips(int count)
    {
        lips = count;
        UIManager.CloseUI(transform);
        if (count <= 0) return;
        float offset = 0f;
        for (int i = 0; i < count; i++)
        {
            GameObject go = UIManager.OpenUI("Lip", null, transform);
            go.transform.localPosition = new Vector3(offset, 0, 0);
            float width = 80; // go.GetComponent<RectTransform>().rect.width;
            offset += width;
        }

        text = UIManager.OpenUI("PrepareText", null, transform);
        text.transform.localPosition = new Vector3(offset, 0, 0);
        text.GetComponent<Text>().text = "x" + count;
    }
    void Update()
    {
    }
    private void OnEnable()
    {
        EventMgr.Instance.AddEvent(EventNameData.LipsEmission, OnLipsEmission);
    }
    private void OnDisable()
    {
        EventMgr.Instance.RemoveEvent(EventNameData.LipsEmission, OnLipsEmission);
    }
    private void OnLipsEmission(object dispatcher, string eventName, object value)
    {
        lips--;
        Debug.Log("Prepare OnLipsEmission: " + lips);
        if (lips == 0)
        {
            Destroy(text);
        }
        else
        {
            text.transform.Translate(Vector3.left * 80, Space.World);
            text.GetComponent<Text>().text = "x" + lips;
        }
        Destroy(transform.GetChild(lips).gameObject);
    }
}