using UnityEngine;
using Libs.Resource;
using UnityEngine.UI;
using Libs.Event;

public class PrepareLipsView : MonoBehaviour
{
    private int lips = 0;
    private void Start()
    {
    }
    private Text text
    {
        get
        {
            return transform.FindChild("number").GetComponent<Text>();
        }
    }
    private Transform lipsTr
    {
        get
        {
            return transform.FindChild("lips");
        }
    }
    public void prepareLips(int count)
    {
        lips = count;
        UIManager.CloseUI(lipsTr);
        if (count <= 0) return;
        float offset = 0f;
        for (int i = 0; i < count; i++)
        {
            GameObject go = UIManager.OpenUI("Lip", null, lipsTr, i);
            go.transform.localPosition = new Vector3(offset, 0, 0);
            float width = go.GetComponent<RectTransform>().rect.width;
            offset += width + 4;
        }
        showNum(lips);
    }
    private void showNum(int count)
    {
        if (text == null) return;
        if (count > 0)
        {
            text.text = "x" + count;
        }
        else
        {
            text.text = "";
        }
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
        if (lips < 0) return;
        Libs.Resource.GameObjectManager.Destroy(lipsTr.GetChild(lips).gameObject);
        showNum(lips);
    }
}