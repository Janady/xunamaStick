using System.Collections.Generic;
using UnityEngine;
using System.Collections;

namespace Libs.Resource
{
    public class GameObjectManager
    {
        private static Dictionary<string, GameObject> resourceMap = new Dictionary<string, GameObject>();
        public static GameObject Instantiate(string resPath, int index = 0, System.Type systemTypeInstance = null)
        {
            string key = resPath + index;
            if (resourceMap.ContainsKey(key))
            {
                GameObject go = resourceMap[key];
                go.SetActive(true);
                return go;
            }
            Object resObject = ResourceManager.LoadResource(resPath, systemTypeInstance);

            if (null != resObject)
            {
                GameObject modelObject = Object.Instantiate(resObject) as GameObject;
                if (null != modelObject)
                {
                    modelObject.name = key;
                }
                resourceMap.Add(key, modelObject);
                return modelObject;
            }

            return null;
        }

        public static GameObject InstantiatePrefabs(string name, int index = 0)
        {
            string fullPath = "Prefabs/" + name;
            return Instantiate(fullPath, index) as GameObject;
        }
        public static void Destroy(GameObject go, float time = 0)
        {
            if (System.Math.Abs(time) < 0.001) go.SetActive(false);
            else Coroutine.CoroutineHandler.Instance().MultiDoCoroutine(destroy(go, time));
        }
        private static IEnumerator destroy(GameObject go, float time)
        {
            yield return new WaitForSeconds(time);
            go.SetActive(false);
            //go.transform.parent = null;
        }
    }
}
