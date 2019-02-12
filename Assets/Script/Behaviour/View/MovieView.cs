using UnityEngine;

using System.IO;
using System.Collections;

public class MovieView : MonoBehaviour
{
    private MediaPlayerCtrl sciMedia;
    private Queue fileQueue;

    // Use this for initialization
    void Start()
    {
        initFiles();
        sciMedia = gameObject.GetComponent<MediaPlayerCtrl>();

        if (sciMedia != null)
        {
            sciMedia.OnEnd += OnEnd;
            loadVideo();
        }
        stop();
    }

    private void OnDisable()
    {
        AppAudioModel.Instance().setAudioActive(true);
    }
    private void OnEnable()
    {
        AppAudioModel.Instance().setAudioActive(false);
    }
    private void initFiles()
    {
        if (!Directory.Exists(Config.Constant.VedioPath))
        {
            Debug.Log(Directory.CreateDirectory(Config.Constant.VedioPath));
        }
        fileQueue = new Queue();
        foreach (FileInfo fi in FileUtil.getVedios(Config.Constant.VedioPath))
        {
            fileQueue.Enqueue("file://" + fi.FullName);
        }
    }
    private void OnEnd()
    {
        loadVideo();
    }
    private void loadVideo()
    {
        if (fileQueue.Count <= 0)
        {
            sciMedia = null;
            gameObject.SetActive(false);
            return;
        }
        string path = fileQueue.Dequeue().ToString();
        fileQueue.Enqueue(path);
        sciMedia.Load(path);
    }
    public void play()
    {
        if (sciMedia != null)
        {
            sciMedia.Play();
            gameObject.SetActive(true);
            transform.SetAsLastSibling();
        }
    }
    public void stop()
    {
        if (sciMedia != null)
        {
            sciMedia.Pause();
            gameObject.SetActive(false);
        }
    }
}
