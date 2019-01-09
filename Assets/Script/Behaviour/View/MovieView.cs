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
    }
    private void OnEnable()
    {
        if (sciMedia != null) sciMedia.Play();
    }
    private void OnDisable()
    {
        if (sciMedia != null) sciMedia.Pause();
    }

    private void initFiles()
    {
        if (!Directory.Exists(Config.Constant.VedioPath))
        {
            Directory.CreateDirectory(Config.Constant.VedioPath);
        }
        fileQueue = new Queue();
        DirectoryInfo direction = new DirectoryInfo(Config.Constant.VedioPath);
        FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
        foreach (FileInfo file in files)
        {
            if (file.Name.EndsWith(".mp4"))
            {
                fileQueue.Enqueue("file://" + file.FullName);
            }
        }
    }
    private void OnEnd()
    {
        loadVideo();
    }
    private void loadVideo()
    {
        if (fileQueue.Count <= 0) gameObject.SetActive(false);
        string path = fileQueue.Dequeue().ToString();
        fileQueue.Enqueue(path);
        sciMedia.Load(path);
        //sciMedia.Play();
        Debug.Log(path);
    }
}
