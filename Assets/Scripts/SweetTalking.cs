using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SweetTalking : MonoBehaviour, MiniGame{
    public GameObject game;
    public AudioSource musicPlay;
    public bool Play;
    public float score = 0;
    public float NeedScore = 0;
    public int Totalnotes = 3;
    public int miss = 0;
    public GameObject WinBar;
    public GameObject WHead;
    public GameObject LHead;
    public GameObject Afool;
    public GameObject pirate;
    public GameObject LoseBar;
    private bool[] HiHat = new bool [10];
    private bool[] Drum = new bool [10];
    private bool[] Bass = new bool [10];
    public NoteObject notesA;
    public NoteObject  notesS;
    public NoteObject notesSpace;
    public Scroller NT;
    public TMP_Text result;
    
    
    private bool gameEnded = false;

    public static SweetTalking instance;
    // Start is called before the first frame update
    private void Start(){
        Initalize(3);
    }

    public void Initalize(int dieValue)
    {
        // scoreText.text = "Score: " + score;
        // NeedText.text = "Needed To Wind: " + NeedScore;
        // missesAllowed.text = "Misses Allowed: " + miss;
        instance = this;
        result.text = "";
        for (int i = 0; i <10; i++){
            HiHat[i] = (Random.Range(0,2) != 0);
            Drum[i] = (Random.Range(0,2) != 0);
            Bass[i] = (Random.Range(0,2) != 0);
        }
         float ypos = notesA.transform.position.y;
         for (int i = 0; i <10; i++){
            Debug.Log(HiHat[i]);
            if (HiHat[i]){
                Totalnotes++;
                NeedScore++;
                // float ypos = Random.Range(-2.6f,35f);
                Vector3 AButton =  new Vector3(notesA.transform.position.x, ypos, notesA.transform.position.z);
                NoteObject newNoteA = Instantiate(notesA, AButton, Quaternion.identity);
                newNoteA.transform.parent = NT.transform;
            }
            Debug.Log(Drum[i]);
            if (Drum[i]){
                Totalnotes++;
                NeedScore++;
                // float ypos = Random.Range(-2.6f,30f);
                Vector3 AButton =  new Vector3(notesS.transform.position.x, ypos, notesS.transform.position.z);
                NoteObject  newNoteS = Instantiate(notesS, AButton, Quaternion.identity);
                newNoteS.transform.parent = NT.transform;
            }
            Debug.Log(Bass[i]);
            if (Bass[i]){
                Totalnotes++;
                NeedScore++;
                // float ypos = Random.Range(-2.6f,30f);
                Vector3 AButton =  new Vector3(notesSpace.transform.position.x, ypos, notesSpace.transform.position.z);
                NoteObject  newNoteSpace = Instantiate(notesSpace, AButton, Quaternion.identity);
                newNoteSpace.transform.parent = NT.transform;
            }
            ypos+=2.5f;
        }
        NeedScore *= 100;
        NeedScore *= 0.5f;
        Debug.Log("NeedScore: " + NeedScore);
        Debug.Log("Total Notes: " + Totalnotes);
        NT.BPM = (dieValue);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameEnded){
            return;
        }
        if (!Play){
            if (Input.anyKeyDown){
                Play = true;
                NT.isStart = true;
                musicPlay.Play();
            }
        }
        if (miss >= 10){
            NT.BPM = 0;
            OnLoss();
            return;
        }
        if (Totalnotes == 0){
            if (score >= NeedScore){
            // NT.BPM = 0;
                OnWin();
                return;
            }
        else{
            OnLoss();
            return;
        }
    }
        
    }

    public void NoteHit() {
        Debug.Log("YASSSS");
        score += 100;
        result.text = score.ToString();
        if (LHead.transform.position.x < pirate.transform.position.x){
            WinBar.transform.localScale = new Vector2(WinBar.transform.localScale.x +0.5f,WinBar.transform.localScale.y);
            LoseBar.transform.localScale = new Vector2(LoseBar.transform.localScale.x +0.5f,LoseBar.transform.localScale.y);
            WHead.transform.position = new Vector2(WHead.transform.position.x +0.5f, WHead.transform.position.y);
            LHead.transform.position = new Vector2(LHead.transform.position.x +0.5f, WHead.transform.position.y);
        }
        Debug.Log(score);
        Totalnotes--;
    }

    public void NoteMiss(){
        Debug.Log("BOOO");
        miss++;
        score -= 100;
        result.text = score.ToString();
        if (WHead.transform.position.x > Afool.transform.position.x){
            WinBar.transform.localScale = new Vector2(WinBar.transform.localScale.x-0.5f,WinBar.transform.localScale.y);
            LoseBar.transform.localScale = new Vector2(LoseBar.transform.localScale.x -0.5f,LoseBar.transform.localScale.y);
            WHead.transform.position = new Vector2(WHead.transform.position.x -0.5f, WHead.transform.position.y);
            LHead.transform.position = new Vector2(LHead.transform.position.x -0.5f, WHead.transform.position.y);
        }
        Debug.Log(miss);
        Totalnotes--;
    }
    
    public void Kill(){
        Destroy(game);
    }


    public void OnWin()
    {
        gameEnded = true;
        // GameManager.Singleton.OnWin();
        result.text = "You Win!";
        Invoke("Kill", 1.5f);
    }

    public void OnLoss()
    {
        gameEnded = true;
        // GameManager.Singleton.OnLoss();
        result.text = "You Lose!";
        Invoke("Kill", 1.5f);
    }

}
