using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SweetTalking : MonoBehaviour
{
    public AudioSource musicPlay;
    public bool Play;
    public Scroller scroll;
    public float score;
    public int miss = 0;
    public SpriteRenderer WinBar;

    public static SweetTalking instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        WinBar.drawMode = SpriteDrawMode.Sliced;
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
        if (miss == 5){
            Debug.Log("Loss");
        }
        if (score == 250){
            Debug.Log("WIN");
        }
        
    }

    public void NoteHit() {
        Debug.Log("YASSSS");
        score += 50;
        WinBar.size += new Vector2(0.6f, 0f);
        Debug.Log(score);
    }

    public void NoteMiss(){
        Debug.Log("BOOO");
        miss++;
        if (WinBar.size.x -0.6 >= 1){
            WinBar.size -= new Vector2(0.6f, 0f);
        }
        Debug.Log(miss);
    }

}
