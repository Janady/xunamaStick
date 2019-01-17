using UnityEngine;
using System.Collections;

public class Effect : MonoBehaviour
{
    public string effectStr;
    private GameObject effect;

    private void OnEnable()
    {
        if (effectStr != null) effect = Libs.Resource.EffectManager.LoadEffect(effectStr, null, false);
    }
    private void OnDisable()
    {
        if (effect != null) Destroy(effect);
    }
}
