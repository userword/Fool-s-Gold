using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupGame : MonoBehaviour
{

    public Sprite cupTilted;

    public GameObject dice;

    public GameObject bar;

    private BarCollisionHandler barCollisionHandler;

    private RectTransform barRect;

    public GameObject redBar; //Outer

    private RectTransform redBarRect;

    public GameObject yellowBar;

    private RectTransform yellowBarRect;

    private YellowBarCollisionHandler yellowBarCollisionHandler;

    public GameObject greenBar; //Center

    private RectTransform greenBarRect;

    private GreenBarCollisionHandler greenBarCollisionHandler;

    float leftBound, rightBound;

    bool forwards = false, backwards = false, shuffling = false, started = false, chosen = false, show = false; // these booleans are supposed to track the different Mini game states

    private float barspeed = 100f;

    private float cupSpeed = 10f;

    public GameObject cup1, cup2, cup3, pirateHand, playerHand;

    private Vector2 cup1Target, cup2Target, cup3Target, pirateTarget, playerTarget;

    public enum GameState { 
    
        
    
    }

    void Awake()
    {

        //keep cups in place

        cup1Target = cup1.transform.position;

        cup2Target = cup2.transform.position;

        cup3Target = cup3.transform.position;

        playerTarget = playerHand.transform.position;

        pirateTarget = pirateHand.transform.position;

        Debug.Log("Cup positions: \n" + cup1.transform.position + " " + cup2.transform.position + " " + cup3.transform.position);

        Debug.Log("Cup targets: \n" + cup1Target + " " + cup2Target + " " + cup3Target);

        // Get components

        barRect = bar.GetComponent<RectTransform>();

        barCollisionHandler = bar.GetComponent<BarCollisionHandler>();

        greenBarRect = greenBar.GetComponent<RectTransform>();

        greenBarCollisionHandler = greenBar.GetComponent<GreenBarCollisionHandler>();

        yellowBarRect = yellowBar.GetComponent<RectTransform>();

        yellowBarCollisionHandler = yellowBar.GetComponent<YellowBarCollisionHandler>();

        redBarRect = redBar.GetComponent<RectTransform>();

        //set bounds for moving black bar

        leftBound = redBarRect.localPosition.x - redBarRect.rect.width / 2;

        rightBound = redBarRect.localPosition.x + redBarRect.rect.width / 2;

        //Debug.Log("left: " + leftBound + "\n" + "right: " + rightBound + "\n");

    }

    IEnumerator ShowDice() {

        started = true;

        Lift(cup2, ref cup2Target, 70f);

        yield return new WaitForSeconds(2);

        Fall(cup2, ref cup2Target, 70f);

        yield return new WaitForSeconds(0.75f);

        dice.transform.SetParent(cup2.transform);

        shuffling = true;

        forwards = true;

        Debug.Log("Cup positions: \n" + cup1.transform.position + " " + cup2.transform.position + " " + cup3.transform.position);

        Debug.Log("Cup targets: \n" + cup1Target + " " + cup2Target + " " + cup3Target);

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.S)) // Show the cups then begin shuffling cups and moving the bar.
        {

            if (!started)
            {

                StartCoroutine(ShowDice());

            } else if (shuffling) {

                shuffling = false;
                backwards = false;
                forwards = false;

                show = true;

                Debug.Log(Status());

                dice.transform.SetParent(null);

            }

        }

        if ( started && !shuffling && cupsStopped() ) {

            chosen = true;
        
        }

        if (chosen == true && show == true) {
            //if green the scam works

            //if yellow the game plays with a 50/chance of winning or losing

            //if red the player tries to pull off the scam but gets caught

            switch (Status()) {

                case "green":
                    

                    break;

                case "yellow":


                    break;

                case "red":


                    break;

            }


            switch ((int)Random.Range(1, 3))
            {

                case 1:
                    Lift(cup1, ref cup1Target, 70f);
                    

                    break;

                case 2:
                    Lift(cup2, ref cup2Target, 70f);
                    

                    break;

                case 3:
                    Lift(cup3, ref cup3Target, 70f);
                    

                    break;

            }

            show = false;

        }

    }





    void FixedUpdate()
    {

        float barX = barRect.localPosition.x;

        //Debug.Log("bar: " + barX);

        if (forwards && shuffling)
        {

            bar.transform.Translate(new Vector2(1f, 0f) * barspeed * Time.deltaTime);

        } else if (backwards) {

            bar.transform.Translate(new Vector2(-1f, 0f) * barspeed * Time.deltaTime);

        }

        if (barX < leftBound) {

            forwards = true;

            backwards = false;

        }

        if (barX > rightBound)
        {

            forwards = false;

            backwards = true;

        }

        if (shuffling)
        {
            if (cupsStopped())
            {

                //if all cups are at thier destination switch them again

                switch ((int)Random.Range(1, 3))
                {

                    case 1:
                        SwitchCups(cup1, cup2, ref cup1Target, ref cup2Target);

                        break;

                    case 2:
                        SwitchCups(cup3, cup2, ref cup3Target, ref cup2Target);

                        break;

                    case 3:
                        SwitchCups(cup1, cup3, ref cup1Target, ref cup3Target);

                        break;

                }

            }

        }

        //move the cups to thier target.

        cup1.transform.position = Vector2.MoveTowards(cup1.transform.position, cup1Target, cupSpeed);

        cup2.transform.position = Vector2.MoveTowards(cup2.transform.position, cup2Target, cupSpeed);

        cup3.transform.position = Vector2.MoveTowards(cup3.transform.position, cup3Target, cupSpeed);



    }

    public void Lift(GameObject cup, ref Vector2 cupTarget, float height) {

        cupTarget = new Vector2(cup.transform.position.x, cup.transform.position.y + height);

    }

    public void Fall(GameObject cup, ref Vector2 cupTarget, float height)
    {

        cupTarget = new Vector2(cup.transform.position.x, cup.transform.position.y - height);

    }

    public bool IsAtTarget(GameObject cup, ref Vector2 cupTarget, float threshold) {
        //checks if the cup has reached its destination 
        if (Vector2.Distance(cup.transform.position, cupTarget) < threshold) {

            return true;

        }

        return false;

    }

    public void SwitchCups(GameObject cup1, GameObject cup2, ref Vector2 cup1target, ref Vector2 cup2target) {

        cup1target = cup2.transform.position;

        cup2target = cup1.transform.position;

    }

    public string Status() {

        if (greenBarCollisionHandler.Status())
        {

            return "green";

        }

        if (yellowBarCollisionHandler.Status()) {

            return "yellow";

        }

        return "red";

    }

    public bool cupsStopped(){

        return IsAtTarget(cup1, ref cup1Target, 0.1f) &&
         IsAtTarget(cup2, ref cup2Target, 0.1f) &&
         IsAtTarget(cup3, ref cup3Target, 0.1f);

        }

}
