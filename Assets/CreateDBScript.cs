﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mod;

public class CreateDBScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartSync();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    private void StartSync()
    {
        Fixture fixture = new Fixture();
        fixture.InitPurchase();
        fixture.InitRecharge();
        fixture.InitCabinet();
        fixture.InitGoods();
        //ToConsole(Purchase.All());
        //ToConsole(Recharge.All());
    }

    private void ToConsole(System.Collections.IEnumerable objects)
    {
        foreach (var obj in objects)
        {
            ToConsole(obj.ToString());
        }
    }

    private void ToConsole(string msg)
    {
        Debug.Log(msg);
    }
}
