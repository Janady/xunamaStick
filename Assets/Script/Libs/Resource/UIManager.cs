using UnityEngine;
using System.Collections;
using System;

namespace Libs.Resource
{
    public class UIManager
    {
        private static GameObject _root;
        
        public static GameObject OpenUI(string name, string dir = null, Transform transform = null, int index = 0)
        {
            string fullPath = "UI/" + (dir == null ? "" : dir + "/") + name;
            GameObject go = GameObjectManager.Instantiate(fullPath, index);
            if (null == go) throw new Exception("load resource null, path = " + fullPath);
            go.name = name;
            AddParent(go, transform);
            return go;
        }

        private static void AddParent(GameObject go, Transform transform = null)
        {
            if (transform == null)
            {
                go.transform.SetParent(root().transform);
                go.GetComponent<RectTransform>().offsetMax = Vector2.zero;
                go.GetComponent<RectTransform>().offsetMin = Vector2.zero;
            }
            else
            {
                go.transform.SetParent(transform);
            }
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
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

        public static void CloseUI(Transform transform = null, string path = null)
        {
            if (transform == null) transform = root().transform;
            int count = transform.childCount;
            for (int i=0; i<count; i++)
            {
                GameObject go = transform.GetChild(i).gameObject;
                if (path == null || go.name.Equals(path))
                {
                    // go.GetComponent<BaseView>(); // custom on close function
                    GameObjectManager.Destroy(go);
                }
            }
        }
        public static void HideUI(Transform transform = null, string path = null)
        {
            if (transform == null) transform = root().transform;
            int count = transform.childCount;
            for (int i = 0; i < count; i++)
            {
                GameObject go = transform.GetChild(i).gameObject;
                if (path == null || go.name.Equals(path))
                {
                    go.SetActive(false);
                }
            }
        }

        public static Sprite GenSprite(Texture2D texture)
        {
            Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            return sprite;
        }
        public static Texture2D loadImage(string filePath, bool local)
        {
            if (local) return ResourceManager.LoadResource(filePath) as Texture2D;
            /*
            byte[] bytes = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D((int)size.x, (int)size.y, TextureFormat.RGB24, false);
            texture.filterMode = FilterMode.Trilinear;
            texture.LoadImage(bytes);
            */
            //return texture;
            //string filePath = "file://" + Application.dataPath + @"/_Image/grid.png";
            Texture2D texture = null;
            WWW www = new WWW("file://" + filePath);
            if (www != null && string.IsNullOrEmpty(www.error))
            {
                texture = www.texture;
            }
            if (www.isDone)
            {
                www.Dispose();
            }
            return texture;
        }
    }
}