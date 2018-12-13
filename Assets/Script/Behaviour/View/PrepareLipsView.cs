using UnityEngine;
using Libs.Resource;
using UnityEngine.UI;

public class PrepareLipsView : MonoBehaviour
{
    private int lips = 0;
    private Vector3 spawn;
    private GameObject text;
    private void Start()
    {
        spawn = new Vector3(0, -8, 0);
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
        if (lips <= 0) return;
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnPin();
        }
    }
    private void SpawnPin()
    {
        useLips();
        GameObject go = Libs.Resource.ResourceManager.InstantiatePrefab("pin");
        go.transform.position = spawn;
    }
    private void useLips()
    {
        lips--;
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