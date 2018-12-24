using UnityEngine;
using System.IO;
using System;

namespace Libs.Resource
{
    public class UIManager
    {
        private static GameObject _root;
        
        public static GameObject OpenUI(string name, string dir = null, Transform transform = null)
        {
            string fullPath = "UI/" + (dir == null ? "" : dir + "/") + name;
            GameObject go = ResourceManager.InstantiateResource(fullPath) as GameObject;
            if (null == go) throw new Exception("load resource null, path = " + fullPath);
            go.name = name;
            AddParent(go, transform);
            return go;
        }

        private static void AddParent(GameObject go, Transform transform = null)
        {
            if (transform == null) transform = root().transform;

            go.transform.SetParent(transform);
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
                    GameObject.Destroy(go);
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
            if (local) return Resources.Load(filePath) as Texture2D;
            /*
            byte[] bytes = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D((int)size.x, (int)size.y, TextureFormat.RGB24, false);
            texture.filterMode = FilterMode.Trilinear;
            texture.LoadImage(bytes);
            */
            //return texture;
            //string filePath = "file://" + Application.dataPath + @"/_Image/grid.png";
            WWW www = new WWW(filePath);
            return www.texture;
        }
    }
}