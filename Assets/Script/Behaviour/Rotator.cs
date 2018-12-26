using UnityEngine;
using System.Collections;
using Libs.Event;
using DG.Tweening;

public class Rotator : MonoBehaviour {
    private const float initYPos = -16f;
    private const float prepareYPos = -8f;
    private const float destYPos = 0f;
    private int total = 0;
    private float speed = 120f;
    private Vector3 spawn;
    private int lips = 0;
    private int receiveLips = 0;
    private bool collided = false;
    private GameObject lip;
    private bool onAir = false;
    // Use this for initialization
    void Start () {
        prepareLips();
    }
    private void OnEnable()
    {
        EventMgr.Instance.AddEvent(EventNameData.LipsCollision, OnLipsCollission);
    }
    private void OnDisable()
    {
        EventMgr.Instance.RemoveEvent(EventNameData.LipsCollision, OnLipsCollission);
    }
    // Update is called once per frame
    void Update () {
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
        if (lips >= total) return;
        if (Input.GetButtonDown("Fire1") && !onAir)
        {
            SpawnPin();
        }
    }
    private void prepareLips()
    {
        GameObject go = Libs.Resource.ResourceManager.InstantiatePrefab("pin");
        go.transform.position = new Vector3(0, initYPos, 0);
        lip = go;
        float interval = 0.1f;
        Tweener tweener = go.transform.DOLocalMoveY(prepareYPos, interval);
        //seq.Append(tweener).AppendCallback(() => {
        //    Received(go);
        //});
        tweener.SetEase(Ease.Linear);
        tweener.OnComplete(() => {
            EventMgr.Instance.DispatchEvent(EventNameData.LipsEmission);
        });
    }
    private void SpawnPin()
    {
        onAir = true;
        lips++;
        float interval = 0.1f;
        Tweener tweener = lip.transform.DOLocalMoveY(destYPos, interval);
        //seq.Append(tweener).AppendCallback(() => {
        //    Received(go);
        //});
        tweener.SetEase(Ease.OutElastic);
        tweener.OnComplete(() => {
            onAir = false;
            Received(lip);
            if (lips < total) prepareLips();
        });
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (total <= 0) return;
        // Handheld.Vibrate();
        if (collision.tag == "Pin")
        {
            //Received(collision.gameObject);
        }
    }
    private void Received(GameObject go)
    {
        if (go == null) return;
        go.transform.SetParent(transform);
        MyShake(transform);
        receiveLips++;
        if (receiveLips >= total)
        {
            StartCoroutine(GamePass());
        }
        if (Random.Range(0, 1f) > 0.6)
        {
            GetComponent<Rotator>().speed *= -1;
        }
    }
    private IEnumerator GamePass()
    {
            yield return new WaitForSeconds(0.5f);
            EventMgr.Instance.DispatchEvent(EventNameData.GamePass, !collided);
            Destroy(gameObject);
    }
    public int Total
    {
        set
        {
            total = value;
            receiveLips = 0;
            lips = 0;
        }
    }
    public float Speed
    {
        set
        {
            speed = value;
        }
    }

    private void MyShake(Transform tf)
    {
        if (tf != null)
        {
            tf.DOShakePosition(0.2f, new Vector3(0, 1, 0), 20);
        }
    }
    private void OnLipsCollission(object dispatcher, string eventName, object value)
    {
        collided = true;
        StartCoroutine(GamePass());
    }
}
