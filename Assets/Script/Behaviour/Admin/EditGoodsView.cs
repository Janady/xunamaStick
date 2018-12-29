using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Mod;
public class EditGoodsView : MonoBehaviour
{
    private Goods _good;
    // Use this for initialization
    void Start()
    {

    }

    public Goods Good
    {
        set
        {
            _good = value;
        }
    }
}
