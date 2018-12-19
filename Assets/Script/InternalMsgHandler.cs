using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;
using System.Threading;

public class InternalMsgHandler : MonoBehaviour
{
    public event Action<byte[]> serialMessageEvent;
    private static InternalMsgHandler _instance = null;
    void Awake()
    {
        _instance = this;
    }

    public static InternalMsgHandler Instance()
    {
        return _instance;
    }

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Home))
        {
            Application.Quit();
        }
    }

    void serial_message(string msg)
    {
        StartCoroutine(serial_handler(msg));
    }

    IEnumerator serial_handler(string msg)
    {
        if (null != serialMessageEvent) serialMessageEvent(Libs.HexString.Hex2bytes(msg));
        yield return null;
    }
}
