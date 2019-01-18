using UnityEngine;
using System.Collections;

public class RotatorSelf : MonoBehaviour
{
    public float speed = 120f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 0f, -speed * Time.deltaTime);
    }
}
