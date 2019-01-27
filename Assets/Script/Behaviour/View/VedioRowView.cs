using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using Libs.Resource;
public class VedioRowView : MonoBehaviour
{
    public enum ActionType
    {
        Delete,
        Add
    }
    private Text titleText;
    private FileInfo _file;
    private Action<FileInfo> callback;
    private ActionType _type;

    void Start()
    {
    }
    public void SetCallBack(Action<FileInfo> callBack, ActionType type)
    {
        Transform tr = transform.FindChild("delete");
        switch (type)
        {
            case ActionType.Add:
                tr.GetComponent<Image>().sprite = UIManager.GenSprite(UIManager.loadImage("Image/Add", true));
                break;
        }
        _type = type;
        Button btn = tr.GetComponent<Button>();
        btn.onClick.AddListener(Onclick);
        callback = callBack;
    }
    void Onclick()
    {
        if (callback != null) callback(_file);
        switch (_type)
        {
            case ActionType.Delete:
                Libs.Resource.GameObjectManager.Destroy(gameObject);
                break;
            case ActionType.Add:
                callback = null;
                Transform tr = transform.FindChild("delete");
                tr.GetComponent<Image>().sprite = UIManager.GenSprite(UIManager.loadImage("Image/ButtonDelete", true));
                break;
        }
    }
    public FileInfo file
    {
        set
        {
            _file = value;
            if (titleText == null)
            {
                titleText = transform.FindChild("name").gameObject.GetComponent<Text>();
            }
            titleText.text = _file.Name;
        }
    }
}
