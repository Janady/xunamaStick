using System.Collections;
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
        var people = Goods.AllGoods();
        ToConsole(people);
    }

    private void ToConsole(IEnumerable<Goods> people)
    {
        foreach (var person in people)
        {
            ToConsole(person.ToString());
        }
    }

    private void ToConsole(string msg)
    {
        Debug.Log(msg);
    }
}
