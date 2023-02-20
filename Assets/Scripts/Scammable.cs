using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scammable : MonoBehaviour
{

    GameController controller;

    public  PirateController myPirate;
    private void Awake()
    {

        controller = GameObject.Find("Main Camera").GetComponent<GameController>();

    }

    public enum scamType { 
    
        Pickpocketing, Lockpicking, CupGame, SweetTalking 

    }

    public scamType typeOfScam;

    public bool avalible = false;

    public float distanceToPlayer;

    public void setPirate(PirateController p) {

        myPirate = p;
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.name == "PlayerParent")
        {

            Debug.Log(collision.gameObject.GetComponent<PlayerMovement>());

            collision.gameObject.GetComponent<PlayerMovement>().RegisterScam(this);

            avalible = true;

        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.name == "PlayerParent")
        {

            distanceToPlayer = Vector2.Distance(this.transform.position, collision.gameObject.transform.position);

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.name == "PlayerParent")
        {

            collision.gameObject.GetComponent<PlayerMovement>().DeclineScam(this);

            avalible = false;

        }

    }
    public void Go() {

        switch (typeOfScam) {

            case scamType.Pickpocketing:
                StartCoroutine(controller.PlayPickpocketingGame());

                break;

            case scamType.Lockpicking:
                StartCoroutine(controller.PlayLockpickingGame());

                break;

            case scamType.CupGame:
                StartCoroutine(controller.PlayCupGame());

                break;
            
            case scamType.SweetTalking:
                StartCoroutine(controller.PlaySweetTalkingGame());

                break;
        }
    }
    
}
