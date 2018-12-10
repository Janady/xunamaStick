using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public GameObject pinPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetButtonDown("Fire1"))
        {
            SpawnPin();
        }
	}

    void SpawnPin()
    {
        Instantiate(pinPrefab, transform.position, transform.rotation);
    }
}
