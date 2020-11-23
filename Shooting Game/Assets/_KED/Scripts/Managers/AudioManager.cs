using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sfx
{
    public string name;
    public AudioClip clip;
}

public class AudioManager : MonoBehaviour
{
    public Sfx[] sfxs;

    Dictionary<string, AudioClip> sfxDictionary = new Dictionary<string, AudioClip>();

    public AudioSource[] audioSources;

    public static AudioManager instance;
    private void Awake()
    {
        if (instance)
            DestroyImmediate(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        for(int i = 0; i < sfxs.Length; i++)
        {
            sfxDictionary.Add(sfxs[i].name, sfxs[i].clip);
        }
    }

    public void PlaySFX(string p_sfxName)
    {
        for(int i = 0; i < audioSources.Length; i++)
        {
            if (!audioSources[i].isPlaying)
            {
                if (sfxDictionary.ContainsKey(p_sfxName))
                {
                    audioSources[i].clip = sfxDictionary[p_sfxName];
                    audioSources[i].Play();
                    break;
                }
            }
        }
    }

}
