using UnityEngine;
using System;

public class ContainerView : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        View.ContainerListView list = transform.FindChild("GoodsList").GetComponent<View.ContainerListView>();
        if (list != null) list.setCallback(x=> {

        });
    }

    // Update is called once per frame
    void Update()
    {

    }
}
