using UnityEngine;
using Libs.Resource;
using System.Collections;

public class ShowEffect : MonoBehaviour
{
    public string effect;
    // Use this for initialization
    void Start()
    {
        EffectManager.LoadEffect(effect);
    }
    private void OnDestroy()
    {
        EffectManager.ReleaseEffect(effect);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
