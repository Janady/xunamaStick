using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Title : MonoBehaviour
{
    public Text text;
    // Use this for initialization
    void Start()
    {

    }

    public string title
    {
        set
        {
            if (text != null) text.text = value;
        }
    }
}
