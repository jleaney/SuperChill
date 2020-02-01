﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public AudioClip backgroundMusic, celebrationMusic;

    public AudioSource sfxAudioSource;
    private static AudioSource musicSource, sfxSource;

    void Awake()
    {
        if (AudioManager.instance == null)
        {
            AudioManager.instance = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        musicSource = GetComponent<AudioSource>();
        sfxSource = sfxAudioSource;
    }

    private void Start()
    {
        StartMusic(backgroundMusic);
    }

    public void StartMusic(AudioClip clip)
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public static void PlaySFXOneShot(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}