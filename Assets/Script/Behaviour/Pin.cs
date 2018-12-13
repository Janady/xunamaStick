using UnityEngine;
using Libs.Event;

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
    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("OnCollisionEnter2D");
        Rigidbody2D r = gameObject.GetComponent<Rigidbody2D>();
        r.velocity = 10 * r.velocity;

        EventMgr.Instance.DispatchEvent(EventNameData.GamePass, false);
    }
}
