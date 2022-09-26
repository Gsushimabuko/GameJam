using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{

    public GameManager gameManager;
    public AudioClip song;
    public int hapinessStatus = 4;
    public AudioSource Source;


    public static MusicManagerScript Instance;

    // Start is called before the first frame update
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
    }
        void Start()
    {
        Source.clip = song;
        Source.Play();
       
    }


   void Update()
    {
        int hapiness = checkHapinessLevel();
        if (hapiness != hapinessStatus)
        {
            if(hapiness == 4)
            {
                hapinessStatus = 4;
                Source.pitch = 1;
            }else if (hapiness == 3)
            {
                hapinessStatus = 3;
                Source.pitch = 0.8f;
            }
            else if (hapiness == 2)
            {
                hapinessStatus = 2;
                Source.pitch = 0.5f;
            }
            else if (hapiness == 1)
            {
                hapinessStatus = 1;
                Source.pitch = 0.2f;
            }
        }
    }
    private int checkHapinessLevel()
    {
       float amount = gameManager.getMoney();

        if(amount > 19000)
        {
            return 4;
        } else if (amount > 10000)
        {
            return 3;
        }
        else if (amount > 5000)
        {
            return 2;
        } else
        {
            return 1;
        }

    }

    /*
    public void SwapTrack(AudioClip newClip)
    {
        StopAllCoroutines();
        StartCoroutine(FadeTrack(newClip));
        Debug.Log("SWAP");
    }

    public IEnumerator FadeTrack(AudioClip nextSong)
    {

        float timeToFade = 1f;
        float timeElapsed = 0;


        if (isPlayingSource1)
        {
            Source2.clip = nextSong;
            Source2.Play();

            while(timeElapsed < timeToFade)
            {
                Debug.Log("FADE");
                Source2.volume = Mathf.Lerp(0, 0.1f, timeElapsed / timeToFade);
                Source1.volume = Mathf.Lerp(0.1f, 0, timeElapsed / timeToFade);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            Source1.Stop();

            
        }
       
    }


    */

}

    
