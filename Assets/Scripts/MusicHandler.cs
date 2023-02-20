using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    public AudioSource musicToPlay;
    public AudioSource chaseMusic;
    public AudioSource loop_A_Music;
    public AudioSource loop_B_Music;
    public bool isChase;
    public bool start;
    public bool letRunA;
    public bool letRunB;
    // Start is called before the first frame update
    void Start()
    {
        musicToPlay.Play();
        start = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (!start && !letRunA){
            loop_A_Music.Play();
            letRunA = true;
            if (!loop_A_Music.isPlaying && !letRunB){
                loop_B_Music.Play();
                letRunB = true;
            }
            if (!loop_B_Music.isPlaying && !loop_A_Music.isPlaying){
                loop_A_Music.Play();
                letRunA = true;
            }
        }
        if (!musicToPlay.isPlaying){
            start = false;
        }
        if (isChase){
            loop_A_Music.Stop();
            loop_B_Music.Stop();
            letRunA = false;
            letRunB = false;
            chaseMusic.Play();
        }
        else if (!isChase){
            chaseMusic.Stop();
        }
        
    }
}
