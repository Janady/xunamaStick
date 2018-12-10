using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour {
    private bool isGameOver = false;
    public Spawner spawner;
    public Rotator rotator;
	// Use this for initialization
	void Start () {
        // Setting.instance.Show();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void GameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;
            spawner.enabled = false;
            rotator.enabled = false;
            Debug.Log("GameOver!");
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
