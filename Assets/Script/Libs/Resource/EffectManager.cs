using System;
using UnityEngine;
using System.Collections;

namespace Libs.Resource
{
    public class EffectManager
    {
        public EffectManager()
        {
        }
        public static GameObject LoadEffect(string name, Transform transform = null)
        {
            string fullPath = "Effect/" + name;
            GameObject go = ResourceManager.InstantiateResource(fullPath) as GameObject;
            if (null == go) throw new Exception("load resource null, path = " + fullPath);
            go.name = name;
            if (transform != null)
            {
                go.transform.position = transform.position;
                go.transform.rotation = transform.rotation;
            }
            return go;
        }
        public static void ReleaseEffect(string name)
        {
            GameObject go = GameObject.Find(name);
            if (go != null)
            {
                UnityEngine.Object.Destroy(go);
            }
        }
    }
}
