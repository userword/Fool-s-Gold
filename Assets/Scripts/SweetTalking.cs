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
    private bool[] HiHat = new bool [10];
    private bool[] Drum = new bool [10];
    private bool[] Bass = new bool [10];
    public Scroller notesA;
    public Scroller notesS;
    public Scroller notesSpace;
    public GameObject NT;
    
    
    

    public static SweetTalking instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        WinBar.drawMode = SpriteDrawMode.Sliced;
        for (int i = 0; i <10; i++){
            HiHat[i] = (Random.Range(0,2) != 0);
            Drum[i] = (Random.Range(0,2) != 0);
            Bass[i] = (Random.Range(0,2) != 0);
        }
         for (int i = 0; i <10; i++){
            Debug.Log(HiHat[i]);
            if (HiHat[i]){
                float ypos = Random.Range(-2.6f,30f);
                Vector3 AButton =  new Vector3(-1.77f, ypos +10.3614767f, 8.329795f);
                Scroller newNoteA = Instantiate(notesA, AButton, Quaternion.identity);
                newNoteA.transform.parent = NT.transform;
            }
            Debug.Log(Drum[i]);
            if (Drum[i]){
                float ypos = Random.Range(-2.6f,30f);
                Vector3 AButton =  new Vector3(0.1f, ypos +10.3614767f, 8.329795f);
                Scroller newNoteS = Instantiate(notesS, AButton, Quaternion.identity);
                newNoteS.transform.parent = NT.transform;
            }
            Debug.Log(Bass[i]);
            if (Bass[i]){
                float ypos = Random.Range(-2.6f,30f);
                Vector3 AButton =  new Vector3(2.4f, ypos +10.3614767f, 8.329795f);
                Scroller newNoteSpace = Instantiate(notesSpace, AButton, Quaternion.identity);
                newNoteSpace.transform.parent = NT.transform;
            }
        }
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
