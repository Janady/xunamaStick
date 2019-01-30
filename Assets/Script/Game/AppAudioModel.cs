using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Libs.Coroutine;
using Libs.Resource;

public class AppAudioName
{
    public static string BGM = "backgroundMusic";
    public static string BGMRAND = "bgm";
    public static string Button1 = "button1";
    public static string Button2 = "button2";
    public static string Coin = "coin";
    public static string Gift = "gift";

    public static string Fail1 = "fail1";
    public static string Fail2 = "fail2";
    public static string Pass = "pass";
    public static string Shot = "shot";
    public static string Success = "success";
}

public class AppAudioModel : MonoBehaviour
{
    //音乐文件
    private AudioSource effect;
    private AudioSource music;

    private float effectVolume;
    private float musicVolume;
    private string musicName = "music";
    private string effectName = "effect";
    private const string APP_AUDIO_PATH = "Audio/";

    private List<string> localAudioNameList = new List<string>();
    private bool audioListFinishStat = true;//音效队列执行完毕标志
    private static AppAudioModel _instance = null;
    void Awake()
    {
        _instance = this;

        effect = this.gameObject.AddComponent<AudioSource>() as AudioSource;
        effect.playOnAwake = false;
        effect.loop = false;
        //设置默认音量	
        effect.volume = 1;

        music = this.gameObject.AddComponent<AudioSource>() as AudioSource;
        //music.clip = Resources.Load("audio/沉睡") as AudioClip;
        music.playOnAwake = false;
        music.loop = true;
        //设置默认音量
        music.volume = 0.5f;
    }

    public static AppAudioModel Instance()
    {
        return _instance;
    }

    void Start()
    {

    }

    private void SetAudioName(string audioName)
    {
        string curAudioPath = APP_AUDIO_PATH + audioName;
        effectName = audioName;
        //Debug.Log("curAudioPath:" + curAudioPath);
        effect.clip = ResourceManager.LoadResource(curAudioPath) as AudioClip;
    }

    public void RunAudio(string audioName)
    {
        if (effect == null)
        {
            return;
        }

        effect.Stop();
        SetAudioName(audioName);
        effect.Play();
    }

    //执行音效队列
    public void RunAudioList(List<string> audioNameList, bool interruptStat = false)
    {
        if (effect == null)
        {
            return;
        }

        if (interruptStat == true)
        {//中断原先队列
            localAudioNameList = audioNameList;
            CoroutineHandler.Instance().DoCoroutine(RunAudioListHandle(), this.GetType().Name, "RunAudioListHandle");
        }
        else
        {
            localAudioNameList.AddRange(audioNameList);//补充音效，不中断原来
            if (audioListFinishStat == true)
            {
                CoroutineHandler.Instance().DoCoroutine(RunAudioListHandle(), this.GetType().Name, "RunAudioListHandle");
            }
        }
    }

    private IEnumerator RunAudioListHandle()
    {
        audioListFinishStat = false;
        while (true)
        {
            if (localAudioNameList.Count == 0)
            {
                break;
            }
            string curAudioName = localAudioNameList[0];
            localAudioNameList.RemoveAt(0);

            effect.Stop();
            SetAudioName(curAudioName);
            effect.Play();
            yield return new WaitForSeconds(effect.clip.length);
        }
        audioListFinishStat = true;
    }

    private void SetMusicName(string audioName)
    {
        string curAudioPath = APP_AUDIO_PATH + audioName;
        musicName = audioName;
        music.clip = Resources.Load(curAudioPath) as AudioClip;
    }

    public void RunMusic(string audioName)
    {
        if (music == null)
        {
            return;
        }
        music.Stop();
        SetMusicName(audioName);
        music.Play();
    }
    public void setAudioActive(bool active, string inputMusicName = null)
    {
        if (music == null)
        {
            return;
        }

        if (effect == null)
        {
            return;
        }

        if (inputMusicName == null)
        {
            if (!active)
            {
                // effect.Stop();
                music.Stop();
            }
            else
            {
                // effect.Play();
                music.Play();
            }
        }
        else
        {
            if (inputMusicName == musicName)
            {
                if (!active)
                {
                    music.Stop();
                }
                else
                {
                    music.Play();
                }
            }
            else if (inputMusicName == effectName)
            {
                if (!active)
                {
                    effect.Stop();
                }
                else
                {
                    effect.Play();
                }
            }
        }
    }

    public void SetEffectVolume(float volume)
    {
        effect.volume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        music.volume = volume;
    }
}