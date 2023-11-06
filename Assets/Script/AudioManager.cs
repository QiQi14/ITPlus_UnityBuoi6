using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Audio[] music;
    public AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public void PlayBGM(string name)
    {
        foreach (var item in music)
        {
            Debug.Log(item.audioName);
        }
        Audio s = Array.Find(music, x => x.audioName == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.audioClip;
            musicSource.Play();
        }
    }

    public void BGMVolume(float value)
    {
        musicSource.volume = value;
    }
}
