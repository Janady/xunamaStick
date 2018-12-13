using UnityEngine;
using Libs.Event;

public class Rotator : MonoBehaviour {
    private int total = 0;
    private int current = 0;
    private float speed = 120f;
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (total <= 0) return;
        Debug.Log("OnTriggerEnter2D: " + collision.tag);
        if (collision.tag == "Pin")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            collision.transform.SetParent(transform);
            current++;
            if (current >= total)
            {
                GamePass();
            }
            if (Random.Range(0, 1f) > 0.6)
            {
                GetComponent<Rotator>().speed *= -1;
            }
        }
    }
    private void GamePass()
    {
        EventMgr.Instance.DispatchEvent(EventNameData.GamePass, true);
        Destroy(gameObject);
    }
    public int Total
    {
        set
        {
            total = value;
            current = 0;
        }
    }
    public float Speed
    {
        set
        {
            speed = value;
        }
    }
}
