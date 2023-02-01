using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetTalking : MonoBehaviour
{
    public AudioSource musicPlay;
    public bool Play;
    public Scroller scroll;

    public static SweetTalking instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Play){
            if (Input.anyKeyDown){
                Play = true;
                scroll.isStart = true;
                musicPlay.Play();
            }
        }
        
    }

    public void NoteHit() {
        Debug.Log("YASSSS");
    }

    public void NoteMiss(){
        Debug.Log("BOOO");
    }
}
