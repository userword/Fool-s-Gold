using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClumsyPirate : MonoBehaviour
{
    /* 
     
    Be pickpocketed

    Be Scammed 
    
    AKA play minigames

    Pirates who you can pick pocket are the ones that are busy at a shop or talking with a mate (potentially)

    The ones you can try to scam are the ones just wandering about
     
     */

    public enum State { 
    
        IDLE, TALKING, SHOPPING, CHASING
    
    }
     
    private State state = State.IDLE;

    private bool playerNear = false;
    void Awake()
    {

        state = State.IDLE;

    }

    void Update()
    {

        if (Input.GetKey(KeyCode.Space)) { 
        
            
        
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {

            playerNear = true;

        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {

            playerNear = false;

        }
    }


}
