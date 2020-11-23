using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Bgm
{
    public string name;
    public AudioClip clip;
}

public class BgmManager : MonoBehaviour
{

    public Bgm[] audioBgm;

    public static BgmManager instance;
    private void Awake()
    {
        if (instance)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    Dictionary<string, AudioClip> bgmTable;

    AudioSource audioMain;
    AudioSource audioSub;

    [Range(0, 1.0f)]
    public float masterVolumn = 1.0f;

    float mainVolumn = .0f;
    float subVolumn = .0f;
    
    [Range(1f, 5f)]
    [SerializeField] float crossFadeTime = 3.0f;


    // Start is called before the first frame update
    void Start()
    {
        bgmTable = new Dictionary<string, AudioClip>
        {
            { audioBgm[0].name, audioBgm[0].clip }
        };

        audioMain = gameObject.AddComponent<AudioSource>();
        audioSub = gameObject.AddComponent<AudioSource>();
        audioMain.volume = .0f;
        audioSub.volume = .0f;
        audioMain.loop = true;
        audioSub.loop = true;
        PlayBGM("Bgm");
    }

    // Update is called once per frame
    void Update()
    {
        if (audioMain.isPlaying)
        {

            if(mainVolumn < 1.0f)
            {
                mainVolumn += Time.deltaTime / crossFadeTime;
                if(mainVolumn >= 1.0f)
                    mainVolumn = 1.0f;
            }


            if (subVolumn > 0.0f)
            {
                subVolumn -= Time.deltaTime / crossFadeTime;
                if (subVolumn <= 0.0f)
                {
                    subVolumn = 0.0f;
                    audioSub.Stop();
                }
                    
            }

        }
        audioMain.volume = mainVolumn * masterVolumn;
        audioSub.volume = subVolumn * masterVolumn;
    }


    public void PlayBGM(string bgmName , bool crossFadeBGM = false)
    {
        if (!bgmTable.ContainsKey(bgmName))
        {
            AudioClip bgm = (AudioClip) Resources.Load("BGM/" + bgmName);

            if (bgm == null) Debug.LogError("BGM Folder Hasn't " + bgmName + " File");

            bgmTable.Add(bgmName, bgm);
        }

        
        if (!crossFadeBGM)
        {
            mainVolumn = 1.0f;
            subVolumn = 0.0f;
        }
        else
        {
            AudioSource temp = audioMain;
            audioMain = audioSub;
            audioSub = temp;

            float tempVolumn = mainVolumn;
            mainVolumn = subVolumn;
            subVolumn = tempVolumn;
        }
        audioMain.clip = bgmTable[bgmName];
        audioMain.Play();
    }


    public void PauseBGM()
    {
        audioMain.Pause();
    }

    public void UnPauseBGM()
    {
        audioMain.UnPause();
    }

    public void StopBGM()
    {
        audioMain.Stop();
    }
}


