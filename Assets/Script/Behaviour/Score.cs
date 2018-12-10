using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Score : MonoBehaviour {
    public static int scoreValue = 0;
    public Text scoreText;
	// Use this for initialization
	void Start () {
        scoreValue = 0;
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = scoreValue.ToString();
	}
}
