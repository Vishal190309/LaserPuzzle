using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static SoundManager instance;
    public static SoundManager Instance { get { return instance; } }
    public AudioSource musicSource;
    public AudioSource effectSource;
    [SerializeField] private SoundType[] soundType;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayMusic(Sound.BACKGROUND_MUSIC);
    }
    AudioClip getAudioClip(Sound sound)
    {
        AudioClip clip = Array.Find(soundType, i => i.soundType == sound).clip;
        if(clip != null)
        {
            return clip;
        }
        else
        {
            return null;
        }
    }
    public void PlayMusic(Sound sound)
    {
        AudioClip clip = getAudioClip(sound);
        if (clip != null) 
        {
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    public void PlaySoundEffect(Sound sound,float Volume=1f)
    {
        AudioClip clip = getAudioClip(sound);
        if (clip != null)
        {
            effectSource.volume = Volume;
            effectSource.PlayOneShot(clip);
        }
    }

}

[Serializable]
public class SoundType
{
    public Sound soundType;
    public AudioClip clip;

}

public enum Sound
{
    BUTTON_CLICK,
    LASER_MIRROR,
    LASER_Wall,
    LEVEL_WIN,
    BACKGROUND_MUSIC
}
