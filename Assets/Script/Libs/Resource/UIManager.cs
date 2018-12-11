using UnityEngine;
using System;

namespace Libs.Resource
{
    public class UIManager
    {
        private static GameObject _root;
        public static GameObject OpenUI(string path)
        {
            string fullPath = "UI/" + path;
            GameObject go = ResourceManager.InstantiateResource(fullPath) as GameObject;
            if (null == go) throw new Exception("load resource null, path = " + fullPath);
            go.name = path;
            AddParent(go);
            return go;
        }

        private static void AddParent(GameObject go)
        {
            go.transform.SetParent(root().transform);
            go.transform.localPosition = Vector3.zero;
            // go.transform.localScale = Vector3.zero;
        }

        private static GameObject root()
        {
            if (_root == null)
            {
                _root = GameObject.Find("Canvas");
                if (_root == null) throw new Exception("Can not find canvas for UI");
            }
            return _root;
        }

        public static void CloseUI(string path = null)
        {
            int count = root().transform.childCount;
            for (int i=0; i<count; i++)
            {
                GameObject go = root().transform.GetChild(i).gameObject;
                if (path == null || go.name.Equals(path))
                {
                    // go.GetComponent<BaseView>(); // custom on close function
                    GameObject.Destroy(go);
                }
            }
        }

        public static Sprite GetSprite(string path)
        {
            Texture2D texture = Resources.Load(path) as Texture2D;
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
    }
}