using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    [SerializeField]
    private AudioClip _bgm;

    [SerializeField]
    private AudioClip[] _minigameSounds;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        bgmSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        bgmSource.loop = true;
        bgmSource.clip = _bgm;
    }

    private void Start()
    {
        bgmSource.volume = 0.1f;
    }

    private void PlayBGM()
    {
        bgmSource.Play();
    }

    public void PlayMinigameSound(int index)
    {
        sfxSource.PlayOneShot(_minigameSounds[index]);
    }


    public void SetMusicVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
    }

    public void PauseSFX()
    {
        sfxSource.Pause();
    }

    public void PauseBGM()
    {
        bgmSource.Pause();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }

    public bool IsPlayingSFX()
    {
        return sfxSource.isPlaying;
    }

    public bool IsPlayingMusic()
    {
        return bgmSource.isPlaying;
    }


    public void UnpauseBGM()
    {
        bgmSource.UnPause();
    }
}
