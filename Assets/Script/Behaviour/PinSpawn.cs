using UnityEngine;

public class PinSpawn : MonoBehaviour
{

    // Use this for initialization
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnPin();
        }
    }
    private void SpawnPin()
    {
        GameObject go = Libs.Resource.ResourceManager.InstantiatePrefab("pin");
        go.transform.position = transform.position;
    }
}
