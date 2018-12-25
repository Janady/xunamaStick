using UnityEngine;
using Libs.Event;
using DG.Tweening;

public class Pin : MonoBehaviour {
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Pin")) return;

        Debug.Log("OnCollisionEnter2D: " + transform.position + collision.transform.position);
        Sequence seq = DOTween.Sequence();;
        if (collision.transform.position.y < transform.position.y) return;
        gameFailed();
        Vector3 rot;
        Vector3 pos;
        if (collision.transform.position.x > 0)
        {
            rot = new Vector3(0, 0, 180);
            pos = new Vector3(-8, -8, 0);
        }
        else
        {
            rot = new Vector3(0, 0, -180);
            pos = new Vector3(8, -8, 0);
        }
        float showTime = 1;
        int rotateNum = 4;
        Tweener t1 = transform.DORotate(rot, showTime/rotateNum).SetLoops(rotateNum);
        Tweener t2 = transform.DOMove(pos, showTime);
        seq.Append(t1).Join(t2);
    }
    private void gameFailed()
    {
        EventMgr.Instance.DispatchEvent(EventNameData.LipsCollision);
    }
}
