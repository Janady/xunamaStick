using System.Collections;
using Libs.Event;
using UnityEngine;
using System;

public class InternalMsgManager : MonoBehaviour {
    private Service.SerialIOService serial = null;
    // Use this for initialization
    void Start () {
        serial = Service.SerialIOService.GetInstance();
        InvokeRepeating("OnCheck", 0, Config.Constant.HEART_BEAT);
        //Invoke("test", 1f);
        //InvokeRepeating("test", 0, Config.Constant.HEART_BEAT);
    }
    private void test()
    {
        //byte[] buf = { (byte)0xAE, (byte)0x06, (byte)0x00, (byte)0x08, (byte)0x00, (byte)0xAF };
        byte[] buf = { (byte)0xAE, (byte)0x06, (byte)0x00, (byte)0x02, (byte)0x02, (byte)0xAF };
        serial.Received(buf);
    }
    private void OnCheck()
    {
        Libs.Api.SerailApi.check();
    }

    void SerialMsg(string msg)
    {
        Debug.Log(msg);
        byte[] buf = Libs.HexString.Hex2bytes(msg);
        serial.Received(buf);
    }
    void chooseFile(string path)
    {
        Debug.Log(path);
        EventMgr.Instance.DispatchEvent(EventNameData.ChooseFile, path);
    }
    private void OnApplicationQuit()
    {
        CancelInvoke("OnCheck");
    }
}
