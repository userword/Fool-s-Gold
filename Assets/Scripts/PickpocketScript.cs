using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * Mechanic:
 *      The Pirate is in the right side of the screen, and the player in the left.
 *      The pirate has three states:
 *      - Idle: Does nothing
 *      - Fidget: The pirate scratches their back or some false alarm
 *      - Looking back: Makes the pirate look straight back. When the player is spotted while moving in this state, they lose.
 *      These are controlled by two variables:
 *      - stateChange: Controls how often a state changes. 0 means that the pirate stays dead still, 1 means they have ADHD and never stay still.
 *      - aggression: During a state change, determines the probability of the pirate looking back. When looking back, probability of going to idle is 1-aggression.
 */

public class PickpocketScript : MonoBehaviour
{

    //SETUP VARIABLES
    [Range(0, 1)]
    public float stateChange; //probability of a state changing from one to another per second(0-1)
    [Range(0, 1)]
    public float aggression; //probability of the state changing to looking back (0-1). Otherwise, transitions to fidgeting.
    public float walk_speed = 10; //determines the movement speed of the player.
    public float distance; //Starting distance between pirate and player.
    public float lookBackDurationMin; //Amount of time pirate looks back minimum.
    public float lookBackDurationMax;//Amount of time pirate looks back maximum.
    public float minigameTimeout; //Amount of time elapsedbefore automatic fail.

    //RUNTIME VARIABLES
    private float metronome; //Calculation for state changes is done every second (to avoid state changes happening too frequently).
    private float elapsed = 0f; //time elapsed since start of minigame. Used for all timings.
    [SerializeField]
    private bool playerWalk = false; //player walking state. True if walking.
    //[SerializeField] (NOT NEEDED?)
    //private int thiefState = 0; // 0 - Idle
    //                            // 1 - Walking
    //                            // 2 - Spotted
    //                            // 3 - Spotted! (Lose)
    [SerializeField]
    private int pirateState = 0; // 0 - Idle
                                 // 1 - Fidget
                                 // 2 - Transition To Look Behind ( for the animation to complete)
                                 // 3 - Look behind
                                 // 4 - Spotted! (Lose)
    //private float pirateTimer; //timer for the pirate to stay in that state.
                               


    //Gameobjects and such
    public GameObject playerObject;
    public GameObject pirateObject;
    private SpriteRenderer playerSprite;
    private SpriteRenderer pirateSprite;
    private Animator playerAnimator;
    private Animator pirateAnimator; //Note: Shares state variables with pirateState.
    private Image playerImage; //To render on Canvas (must be image!)
    private Image pirateImage; //To render on Canvas



    // Initialize the positions of the pirate from the player.
    //TURN TO "Initialize" ONCE COMPLETED
    void Awake()
    {

        //Get gameobject variables.
        playerSprite = playerObject.GetComponent<SpriteRenderer>();
        pirateSprite = pirateObject.GetComponent<SpriteRenderer>();
        pirateAnimator = pirateObject.GetComponent<Animator>();
        playerAnimator = playerObject.GetComponent<Animator>();
        playerImage = playerObject.GetComponent<Image>();
        pirateImage = pirateObject.GetComponent<Image>();


    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        metronome += Time.deltaTime;

        if (elapsed > minigameTimeout) //went past the time limit.
        {
            //Failure state.
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            //Move player to the right based on walkspeed * deltatime
            // e.g. player.transform.position += walkspeed * Time.deltaTime;
            //And set the animation to sneaking
            //print("WALKING");
            playerWalk = true;
            playerObject.transform.position += new Vector3(walk_speed * Time.deltaTime, 0, 0);
            playerAnimator.SetInteger("PlayerAnimState", 1); //set animation to walking.
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            //Player no longer walking
            //Set animation to idling
            playerWalk = false;
            playerAnimator.SetInteger("PlayerAnimState", 0); //Set animation to idling.
        }


        float rng = Random.value; //used for determining whether to change state.

        if (pirateState == 0) //idling
        {
            if (rng < stateChange && metronome >= 1) //state will change!
            {
                metronome = 0; //reset the metronome.
                float rng2 = Random.value; //used for determining whether to fidget or look back.
                if (rng2 < aggression) //look back!
                {
                    
                    pirateState = 2; //Transition to look behind
                    pirateAnimator.SetInteger("PirateAnimState", 2); //Trigger Look Behind animation.
                    //trigger animation change to look back here.
                    //The animation event will call ChangePirateState() and change pirateState to the appropriate value.
                    //pirateTimer = Random.Range(lookBackDurationMin, lookBackDurationMax); //init timer for look back.
                }
                else //fidget!
                {
                    pirateState = 1; //fidgeting.
                    pirateAnimator.SetInteger("PirateAnimState", 1); //Trigger fidgeting animation.
                    //trigger uneventful animation to fidgeting.
                }

            }
        }
        else if (pirateState == 1) //fidgeting
        {
            if (rng < stateChange && metronome >= 1) //state will change!
            {
                metronome = 0; //reset metronome to 0
                float rng2 = Random.value; //used for determining whether to fidget or look back.
                float aggression2 = aggression + ((1 - aggression) * 0.25f); //When fidgeting, aggression is increased!
                if (rng2 < aggression2) //look back!
                {
                    pirateState = 2; //Transition to look behind
                    pirateAnimator.SetInteger("PirateAnimState", 2); //Trigger Look Behind animation.
                    //trigger animation change to look back here.
                    //The animation event will call ChangePirateState() and change pirateState to the appropriate value.
                    //pirateTimer = Random.Range(lookBackDurationMin, lookBackDurationMax); //init timer for look back.
                }
                else //Return to idle.
                {
                    pirateState = 0;
                    pirateAnimator.SetInteger("PirateAnimState", 0);
                    //trigger uneventful animation to idle.
                }

            }
        } 
        else if (pirateState == 3) //looking back (after transition; nothin done when pirateState == 2)
        {
            print("HERE");
            //print(pirateTimer);
            //if (pirateTimer <= 0)
            //{
            //    //transition to idle again.
            //    pirateTimer = 0;
            //    metronome = 0;
            //    print("RETURN: " + pirateTimer);
            //    pirateState = 0; //idle
            //    pirateAnimator.SetInteger("PirateAnimState", 0);
            //}
            if (rng < 1-aggression && metronome >= 1) //state will change!
            {
                //There can only be transition to idle from looking back.
                metronome = 0; //reset metronome to 0
                pirateState = 0;
                pirateAnimator.SetInteger("PirateAnimState", 0);
                //trigger uneventful animation to idle.

            }
            if (playerWalk == true)
            {
                
                //Trigger fail state here
                print("FAIL!!!!");
                //Start spot animation for player and pirate.
                pirateState = 4;
                pirateAnimator.SetInteger("PirateAnimState", 4);
                playerAnimator.SetInteger("PlayerAnimState", 2);
            }
            //pirateTimer -= Time.deltaTime;
        }
        else if (pirateState == 4) //Spotted! (failed)
        {
            print("SPOTTED");
            //Do something here?
        }

        //Update the images of all objects.
        playerImage.sprite = playerSprite.sprite;
        pirateImage.sprite = pirateSprite.sprite;
        //print("END");
    }

    //Function to be called by animation event (once the transition completes to look back.
    public void ChangePirateState(int value)
    {
        pirateState = value;
    }

}
