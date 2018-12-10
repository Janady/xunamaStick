using UnityEngine;
using System.Collections;

public class Pin : MonoBehaviour {
    private float speed = 100f;
    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.up * speed;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Rotator")
        {
            Score.scoreValue++;
            rb.velocity = Vector2.zero;
            transform.SetParent(collision.transform);
            if (Random.Range(0,1f) > 0.6)
            {
                collision.GetComponent<Rotator>().speed *= -1;
            }
        }
        else if (collision.tag == "Pin")
        {
            GameObject.FindObjectOfType<GameManager>().GameOver();
            // float lAngle = Vector3.Angle(transform.up, Vector3.right);
            // transform.Rotate(Vector3.forward * 2.0f * lAngle * fFlag);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
        Rigidbody2D r = gameObject.GetComponent<Rigidbody2D>();
        r.velocity = 10 * r.velocity;
        // GameObject.FindObjectOfType<GameManager>().GameOver();
    }
}
