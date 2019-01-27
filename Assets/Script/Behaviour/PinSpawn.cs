using UnityEngine;

public class PinSpawn : MonoBehaviour
{

    // Use this for initialization
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            //SpawnPin();
        }
    }
    private void SpawnPin()
    {
        GameObject go = Libs.Resource.GameObjectManager.InstantiatePrefabs("pin");
        go.transform.position = transform.position;
    }
}
