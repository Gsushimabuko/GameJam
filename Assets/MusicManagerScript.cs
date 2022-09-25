using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{

    public GameManager gameManager;
    public AudioSource newSong;
    public AudioSource nextSong;


    private bool isPlayingTrack;


    public static MusicManagerScript instance;

    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    void Start()
    {
        newSong.Play();  
        isPlayingTrack = true;
    }

   void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SwapTrack(this.nextSong);
        }
    }

    public void SwapTrack(AudioSource newClip)
    {
        if (isPlayingTrack)
        {
            newSong.Stop();
            nextSong.Play();
        }
        else
        {

        }
    }




}

    
