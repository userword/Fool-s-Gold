using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour
{

    GameController GC;

    public GameObject chestPrefab;
    public GameObject myChest;
    public enum pirateState {

        WANDERING, SEARCHING, CHASING, CHATTING, DRUNK, CARRYING, STILL

    }
    public enum direction {

        UP, DOWN, LEFT, RIGHT, STILL

    }

    private int bumps;

    private int bumpLimit = 5;

    private direction movementDirection;

    public pirateState state;

    private float speed = 1;

    Vector3 movement;

    public BuildingColliderHandler leftCollision;

    public BuildingColliderHandler rightCollision;

    public BuildingColliderHandler upperCollision;

    public BuildingColliderHandler lowerCollision;

    public PirateSightHandler sightHandler;

    public ChatboxHandler chatHandler;

    public Transform sightTransform;

    public Animator anim;

    GameObject newChest;

    void Awake()
    {

        GC = GameObject.Find("Main Camera").GetComponent<GameController>();

        bumps = 0;

        SetDirecton(direction.UP);

        state = pirateState.WANDERING;

    }
    void Update()
    {

        if (bumps == bumpLimit && myChest.activeSelf) {

            DropChest();

        }

        switch (state)
        {

            case pirateState.WANDERING:


                anim.SetInteger("PirateAnimState", 0);

                TurnAwayFromWall();

                if (chatHandler.chatting) {

                    state = pirateState.CHATTING;

                }

                break;

            case pirateState.SEARCHING:

                anim.SetInteger("PirateAnimState", 0);

                speed = 1.1f;

                TurnAwayFromWall();

                if (sightHandler.found)
                {
                    state = pirateState.CHASING;
                }

                break;

            case pirateState.CHASING:

                anim.SetInteger("PirateAnimState", 2);

                speed = 1.5f;

                movement = (sightHandler.TargetPos - (Vector2)transform.position).normalized;

                sightTransform.right = sightHandler.TargetPos - (Vector2)transform.position;

                if (!sightHandler.found)
                {
                    state = pirateState.SEARCHING;
                }

                break;

            case pirateState.CHATTING:
                anim.SetInteger("PirateAnimState", 3);

                state = pirateState.STILL;

                if (!chatHandler.chatting)
                {

                    state = pirateState.WANDERING;

                    SetDirecton(direction.LEFT);

                }

                if (chatHandler.RXmarks.activeSelf == true)
                {

                    sightTransform.eulerAngles = new Vector3(0f, 0f, 0f);

                }
                else {

                    sightTransform.eulerAngles = new Vector3(0f, 0f, 180f);

                }

                break;

            case pirateState.DRUNK:

                anim.SetInteger("PirateAnimState", 1);

                speed = 0.5f;

                TurnAwayFromWall();

                break;

            case pirateState.CARRYING:

                anim.SetInteger("PirateAnimState", 0);

                break;

            case pirateState.STILL:

                anim.SetInteger("PirateAnimState", 0);

                SetDirecton(direction.STILL);

                if (newChest != null)
                {

                    if (newChest.activeSelf == false)
                    {

                        SetMode(pirateState.WANDERING);

                        sightTransform.eulerAngles = new Vector3(0f, 0f, 180f);


                    }
                }

                break;

        }
    }

    private void FixedUpdate()
    {

        transform.Translate(movement * speed * Time.deltaTime);

    }

    public void SetDirecton(direction newDirection) {

        movementDirection = newDirection;

        switch (newDirection) {

            case direction.UP:

                movement = new Vector3(0, 1, 0);

                sightTransform.eulerAngles = new Vector3(0f, 0f, 90f);

                break;

            case direction.DOWN:

                movement = new Vector3(0, -1, 0);

                sightTransform.eulerAngles = new Vector3(0f, 0f, 270f);

                break;

            case direction.LEFT:

                movement = new Vector3(-1, 0, 0);

                sightTransform.eulerAngles = new Vector3(0f, 0f, 180f);

                break;

            case direction.RIGHT:

                sightTransform.eulerAngles = new Vector3(0f, 0f, 0f);

                movement = new Vector3(1, 0, 0);

                break;

            case direction.STILL:

                movement = new Vector3(0, 0, 0);

                break;

        }

    }

    IEnumerator LookingAround() {

        yield return new WaitForSeconds(4);

        SetMode(pirateState.STILL);
    
    }
    IEnumerator Chat()
    {

        yield return new WaitForSeconds(10);

        SetMode(pirateState.WANDERING);

    }


    public void DropChest() {

        myChest.SetActive(false);

        newChest = Instantiate(chestPrefab);

        newChest.transform.position = this.transform.position;

        newChest.GetComponent<Scammable>().setPirate(this);
    
    }

    public void Anger() {

        //pirate should shake for a few secs then, look for the player

        state = pirateState.SEARCHING;
    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.name == "PlayerParent")
        {
            if (state == pirateState.CHASING && GameController.frozen == false) {

                StartCoroutine(GC.PlaySweetTalkingGame());



            }

        }

    }
    public void SetMode(pirateState state) {

        this.state = state;

    }
    public void TurnAwayFromWall() {

        if (rightCollision.contact == true && movementDirection == direction.RIGHT)
        {
            bumps++;
            if (Random.Range(0, 2) == 0) {

                SetDirecton(direction.UP);

            }
            else
            {

                SetDirecton(direction.DOWN);
            }

        }

        if (leftCollision.contact == true && movementDirection == direction.LEFT)
        {
            bumps++;

            if (Random.Range(0, 2) == 0)
            {

                SetDirecton(direction.DOWN);

            } else {

                SetDirecton(direction.UP);

            }

        }

        if (upperCollision.contact == true && movementDirection == direction.UP)
        {
            bumps++;

            if (Random.Range(0, 2) == 0)
            {

                SetDirecton(direction.LEFT);

            } else {

                SetDirecton(direction.RIGHT);
            }

        }

        if (lowerCollision.contact == true && movementDirection == direction.DOWN)
        {
            bumps++;

            if (Random.Range(0, 2) == 0)
            {

                SetDirecton(direction.RIGHT);

            }
            else
            {
                SetDirecton(direction.LEFT);


            }

        }

    }

}
