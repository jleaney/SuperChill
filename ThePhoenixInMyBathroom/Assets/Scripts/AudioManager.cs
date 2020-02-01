using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;

    public AudioClip backgroundMusic, celebrationMusic;
    private static AudioClip celebrationMusicStatic;

    public AudioSource sfxAudioSource;
    private static AudioSource musicSource, sfxSource;

    public AudioClip[] placementSounds;
    public static AudioClip[] placementSoundsStatic;

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

        celebrationMusicStatic = celebrationMusic;
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

    // plays single sound
    public static void PlaySFXOneShot(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // chooses random sound from array and plays it
    public static void PlaySFXOneShot(AudioClip[] clips)
    {
        int clipToPlay = Random.Range(0, clips.Length);
        sfxSource.PlayOneShot(clips[clipToPlay]);
    }

    // used for stickers, googley eyes etc.
    public static void PlayPlacementSound()
    {
        PlaySFXOneShot(placementSoundsStatic);
    }

    public static void StartExhibition()
    {
        musicSource.Stop();
        musicSource.clip = celebrationMusicStatic;
        musicSource.Play();
    }
}
