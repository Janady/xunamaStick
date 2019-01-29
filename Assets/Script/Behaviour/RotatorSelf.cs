using UnityEngine;
using System.Collections;

public class RotatorSelf : MonoBehaviour
{
    public Transform tr;
    public float speed = 120f;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (tr != null) tr.Rotate(0f, 0f, -speed * Time.deltaTime);
    }
}
