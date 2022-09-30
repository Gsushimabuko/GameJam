using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    private AudioSource bgmSource;
    private AudioSource sfxSource;

    public AudioClip clickSound;
    public AudioClip newEventSound;

    [SerializeField]
    private AudioClip _bgm;

    [SerializeField]
    private AudioClip[] _minigameSounds;
    
    public AudioClip minigameWin;
    public AudioClip minigameLoss;
    public AudioClip minigameCounter;

    public AudioClip moneyLoss;
    public AudioClip moneyWin;

    [SerializeField]
    private AudioClip[] _hapinessTracks;

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
        sfxSource.volume = 0.2f;
        PlayBGM();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            sfxSource.PlayOneShot(clickSound);
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

    public void playClick()
    {
       sfxSource.PlayOneShot(clickSound);
    }

    public void playMoneyWin()
    {
        sfxSource.PlayOneShot(this.moneyWin);
    }
    public void playMoneyLoss()
    {
        sfxSource.PlayOneShot(this.moneyLoss);
    }
    public void playMinigameWin()
    {
        sfxSource.PlayOneShot(this.minigameWin);
    }
    public void playMiniGameLoss()
    {
        sfxSource.PlayOneShot(this.minigameLoss);
    }

    public void PlayNewEventSound()
    {
        sfxSource.PlayOneShot(newEventSound);
    }

    public void FadeInBGM()
    {
        StartCoroutine("FadeIn");
    }

    IEnumerator FadeIn()
    {
        for (float i = 0.1f; i >= 0; i = i - 0.01f)
        {
            bgmSource.volume = i;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void FadeOutBGM()
    {
        StartCoroutine("FadeOut");
    }

    IEnumerator FadeOut()
    {
        for (float i = 0; i <= 0.1f; i = i + 0.01f)
        {
            bgmSource.volume = i;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
