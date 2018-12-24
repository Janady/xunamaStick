using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InternalMsgManager : MonoBehaviour {

    public event Action<byte[]> serialMessageEvent;
    private static InternalMsgManager _instance = null;
    private Service.SerialIOService serial = null;
    void Awake()
    {
        _instance = this;
    }
    // Use this for initialization
    void Start () {
        serial = Service.SerialIOService.GetInstance();
    }
	
	// Update is called once per frame
	void Update () {
	}

    void SerialMsg(string msg)
    {
        Debug.Log(msg);
        byte[] buf = Libs.HexString.Hex2bytes(msg);
        serial.Received(buf);
    }
}
