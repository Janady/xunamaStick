using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Title : MonoBehaviour
{
    public Text text;
    public Text desc;
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
    public string description
    {
        set
        {
            if (desc != null) desc.text = value;
        }
    }
}
