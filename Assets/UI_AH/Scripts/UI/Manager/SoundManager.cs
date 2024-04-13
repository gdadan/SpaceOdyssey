using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;
    public AudioSource sfxAudioSource2;

    public List<AudioClip> bgmClips;

    public List<AudioClip> sfxClips;

    public List<AudioClip> sfxClips2;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        BGMStart(0);
    }

    public void BGMStart(int index)
    {
        bgmAudioSource.clip = bgmClips[index];
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    public void BGMStop()
    {
        bgmAudioSource.Stop();
    }

    public void PlaySFX(int index)
    {
        sfxAudioSource.PlayOneShot(sfxClips[index]);
    }

    public void PlaySFX2(int index)
    {
        sfxAudioSource2.PlayOneShot(sfxClips2[index]);
    }
}
