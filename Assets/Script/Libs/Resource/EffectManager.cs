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
        public static void ReleaseEffect(string name)
        {
            GameObject go = GameObject.Find(name);
            if (go != null)
            {
                UnityEngine.Object.Destroy(go);
            }
        }

        public static GameObject LoadEffect(string name, Transform tr = null, bool autoDestroy = true)
        {
            if (null == name) throw new Exception("load resource null, name = " + name);
            string fullPath = "Effect/" + name;
            GameObject go = ResourceManager.InstantiateResource(fullPath) as GameObject;
            if (null == go) throw new Exception("load resource null, path = " + fullPath);

            if (tr)
            {
                go.transform.position = tr.position;
                //go.transform.rotation = tr.rotation;
            }

            ParticleSystem m_ExplosionParticles = go.GetComponent<ParticleSystem>();
            if (m_ExplosionParticles == null)
            {
                for (int i = 0; i < go.transform.childCount; i++)
                {
                    m_ExplosionParticles = go.transform.GetChild(i).GetComponent<ParticleSystem>();
                    if (m_ExplosionParticles != null) break;
                }
            }
            m_ExplosionParticles.Play();
            if (autoDestroy)
                GameObject.Destroy(go, m_ExplosionParticles.duration);
            return go;
        }
    }
}
