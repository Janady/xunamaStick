using UnityEngine;
using Libs.Resource;
using UnityEngine.UI;

public class PrepareLipsView : MonoBehaviour
{
    private int lips = 0;
    private Vector3 spawn;
    private void Start()
    {
        spawn = new Vector3(0, -8, 0);
    }
    public void prepareLips(int count)
    {
        lips = count;
        UIManager.CloseUI(transform);
        float offset = 0f;
        for (int i = 0; i < count; i++)
        {
            GameObject go = UIManager.OpenUI("Lip", null, transform);
            go.transform.localPosition = new Vector3(offset, 0, 0);
            float width = 80; // go.GetComponent<RectTransform>().rect.width;
            offset += width;
        }
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
        Destroy(transform.GetChild(lips).gameObject);
    }
}