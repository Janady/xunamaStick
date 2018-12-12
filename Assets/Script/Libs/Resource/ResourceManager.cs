using UnityEngine;

namespace Libs.Resource
{
    public class ResourceManager : MonoBehaviour
    {
        public static Object LoadResource(string resPath, System.Type systemTypeInstance = null)
        {
            Object resObject = null;
            if (null == systemTypeInstance)
            {
                resObject = Resources.Load(resPath);
            }
            else
            {
                resObject = Resources.Load(resPath, systemTypeInstance);
            }

            return resObject;
        }

        public static Object InstantiateResource(string resPath, string szKey = "", System.Type systemTypeInstance = null)
        {
            Object resObject = null;
            if (null == systemTypeInstance)
            {
                resObject = Resources.Load(resPath);
            }
            else
            {
                resObject = Resources.Load(resPath, systemTypeInstance);
            }

            if (null != resObject)
            {
                Object modelObject = (GameObject)GameObject.Instantiate(resObject);
                if (null != modelObject && szKey.Length > 0)
                {
                    modelObject.name = szKey;
                }
                return modelObject;
            }

            return null;
        }

        // 销毁一个实例对象GameObject，引用记数-1，对象不会自动置空。
        public static void DestroyResource(GameObject obj, bool bImmediate = false)
        {
            if (null != obj)
            {
                if (false == bImmediate)
                {
                    GameObject.Destroy(obj);
                }
                else
                {
                    GameObject.DestroyImmediate(obj);
                }
            }
        }

        // 销毁一个实例对象GameObject，引用记数-1，对象会自动置空。
        public static void DestroyResource(ref GameObject obj, bool bImmediate = false)
        {
            if (null != obj)
            {
                if (false == bImmediate)
                {
                    GameObject.Destroy(obj);
                }
                else
                {
                    GameObject.DestroyImmediate(obj);
                }
                obj = null;
            }
        }

        public static GameObject InstantiatePrefab(string name)
        {
            string fullPath = "Prefabs/" + name;
            return InstantiateResource(fullPath) as GameObject;
        }
    }
}