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
    private GameFacts _fact;
    private Vector3 spawn;
    private int lips = 0;
    private int receiveLips = 0;
    private bool collided = false;
    private GameObject lip;
    private bool onAir = false;
    // Use this for initialization
    void Start() {
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
    void Update()
    {
        changeSpeed(false);
        transform.Rotate(0f, 0f, speed * Time.deltaTime);
        if (lips >= total) return;
        if (Input.GetButtonDown("Fire1") && !onAir)
        {
            SpawnPin();
            AppAudioModel.Instance().RunAudio(AppAudioName.Shot);
        }
    }
    private IEnumerator prepareLips()
    {
        yield return new WaitForEndOfFrame();
        GameObject go = Libs.Resource.GameObjectManager.InstantiatePrefabs("pin", lips);
        go.transform.parent = transform.parent;
        go.transform.localRotation = Quaternion.identity;
        go.transform.position = new Vector3(0, initYPos, 0);
        lip = go;
        yield return new WaitForEndOfFrame();
        float interval = 0.02f;
        Tweener tweener = go.transform.DOLocalMoveY(prepareYPos, interval);
        //seq.Append(tweener).AppendCallback(() => {
        //    Received(go);
        //});
        tweener.SetEase(Ease.Linear);
        tweener.OnComplete(() => {
            onAir = false;
            EventMgr.Instance.DispatchEvent(EventNameData.LipsEmission);
        });
    }
    private void SpawnPin()
    {
        onAir = true;
        lips++;
        float interval = 0.05f;
        Tweener tweener = lip.transform.DOLocalMoveY(destYPos, interval);
        //seq.Append(tweener).AppendCallback(() => {
        //    Received(go);
        //});
        tweener.SetEase(Ease.Linear);
        tweener.OnComplete(() => {
            Received(lip);
            if (lips < total)
                StartCoroutine(prepareLips());
        });
    }

    private void Received(GameObject go)
    {
        if (go == null) return;
        go.transform.SetParent(transform);
        MyShake(transform);
        receiveLips++;
        Libs.Resource.EffectManager.LoadEffect("boom", go.transform);
        if (receiveLips >= total)
        {
            StartCoroutine(GamePass());
        }
        changeSpeed(true);
    }
    private IEnumerator GamePass()
    {
        yield return new WaitForSeconds(0.5f);
        // clear();
        // yield return new WaitForEndOfFrame();
        EventMgr.Instance.DispatchEvent(EventNameData.GamePass, !collided);
    }
    private void clear()
    {
        Libs.Resource.GameObjectManager.Destroy(gameObject);
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("Pin"))
        {
            Debug.Log("destroy: " + go.name);
            Libs.Resource.GameObjectManager.Destroy(go);
        }
    }
    int changeCount = 0;
    private void changeSpeed(bool force)
    {
        if (changeCount++ > 50 || force)
        {
            changeCount = 0;
            speed = (_fact == null) ? 0 : _fact.Speed;
        }
    }
    public GameFacts Fact
    {
        set
        {
            _fact = value;
            total = value.Count;
            receiveLips = 0;
            lips = 0;
            collided = false;
            StartCoroutine(prepareLips());
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
