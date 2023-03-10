using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class CupGame : MonoBehaviour, MiniGame
{

    public Sprite cupTilted;

    public Sprite cupRegular;

    public GameObject dice;

    public GameObject bar;

    private BarCollisionHandler barCollisionHandler;

    private RectTransform barRect;

    public GameObject redBar; // Outer

    private RectTransform redBarRect;

    public GameObject yellowBar;

    private RectTransform yellowBarRect;

    private YellowBarCollisionHandler yellowBarCollisionHandler;

    public GameObject greenBar; // Center

    private RectTransform greenBarRect;

    private GreenBarCollisionHandler greenBarCollisionHandler;

    float leftBound, rightBound;

    bool forwards = false, backwards = false, shuffling = false, started = false, chosen = false, show = false; // these booleans are supposed to track the different Mini game states

    private float barspeed = 500f;

    private float cupSpeed = 10f;

    private int rollResult;

    public GameObject cup1, cup2, cup3, pirateHand, playerHand;

    public RectTransform cup1Rect, cup2Rect, cup3Rect, pirateHandRect, playerHandRect;

    private Vector2 cup1Target, cup2Target, cup3Target, pirateTarget, playerTarget;

    public void Initalize(int dieValue)
    {

        // keep cups in place

        cup1Target = cup1Rect.localPosition;

        cup2Target = cup2Rect.localPosition;

        cup3Target = cup3Rect.localPosition;

        playerTarget = playerHandRect.localPosition;

        pirateTarget = pirateHandRect.localPosition;

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

        // set bounds for moving black bar

        leftBound = redBarRect.localPosition.x - redBarRect.rect.width / 2;

        rightBound = redBarRect.localPosition.x + redBarRect.rect.width / 2;

        float newCenter = UnityEngine.Random.Range(leftBound + (yellowBarRect.rect.width / 2), rightBound - (yellowBarRect.rect.width / 2));

        yellowBarRect.localPosition = new Vector3(newCenter, yellowBarRect.localPosition.y, yellowBarRect.localPosition.z);

        greenBarRect.localPosition = new Vector3(newCenter, yellowBarRect.localPosition.y, yellowBarRect.localPosition.z);

        // Set size and position of smaller bars based on die roll, and speed of selection bar.

        Debug.Log("Rolled a " + dieValue);

        yellowBar.transform.localScale = new Vector3((dieValue * 0.075f), 1f, 1f);

        greenBar.transform.localScale = new Vector3((dieValue * 0.05f), 1f, 1f);

        barspeed -= dieValue * 30f;

    }

    public void OnWin()
    {

        // GameManager.Singleton.OnWin();

        // Destroy(gameObject);

    }

    public void OnLoss()
    {

        // GameManager.Singleton.OnLoss();

        // Destroy(gameObject);

    }

    IEnumerator ShowDice() {

        Debug.Log("Showing Dice");

        started = true;

        Lift(cup2Rect, ref cup2Target, 70f);

        yield return new WaitForSeconds(2);

        Fall(cup2Rect, ref cup2Target, 70f);

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


            }

        }

        if ( started && !shuffling && cupsStopped() ) {

            chosen = true;
        
        }

        if (chosen == true && show == true) {

            Debug.Log("chose");

            show = false;

            //if green the scam works

            //if yellow the game plays with a 50/chance of winning or losing

            //if red the player tries to pull off the scam but gets caught

            switch (Status())
            {

                case "green":

                    Debug.Log(Status());
                    StartCoroutine(Yoink());

                    break;

                case "yellow":

                    Debug.Log(Status());
                    StartCoroutine(pickRight());

                    break;

                case "red":
                    Debug.Log(Status());
                    StartCoroutine(pickWrong());

                    break;

            }
        }

    }
    IEnumerator Yoink()
    {
        // player win

        Debug.Log("Yoink");

        playerTarget = cup2Rect.localPosition;

        cup2.GetComponent<Image>().sprite = cupTilted;

        dice.GetComponent<Canvas>().sortingOrder = 30;

        yield return new WaitUntil(() => Vector2.Distance(playerHandRect.localPosition, playerTarget) < 0.1f);

        Debug.Log("Arrived");

        dice.transform.SetParent(playerHand.transform);

        //Destroy(dice);

        playerTarget = new Vector3(playerHandRect.localPosition.x, -1000f, 0f);

        yield return new WaitUntil(() => Vector2.Distance(playerHandRect.localPosition, playerTarget) < 0.1f);

        StartCoroutine(pickRight());

    }


    IEnumerator pickWrong() {

        Debug.Log("Wrong");

        pirateTarget = cup2Rect.localPosition + new Vector3(0f, 10f, 0f);

        yield return new WaitUntil(() => Vector2.Distance(pirateHandRect.localPosition, pirateTarget) < 0.1f);

        Debug.Log("Arrived");

        dice.transform.SetParent(this.transform);

        cup1Target = cup1Rect.localPosition + new Vector3(0f, 70f, 0f);

        pirateTarget = pirateHandRect.localPosition + new Vector3(0f, 70f, 0f);

        yield return new WaitUntil(() => Vector2.Distance(pirateHandRect.localPosition, pirateTarget) == 0f);

        yield return new WaitForSeconds(3);

        OnWin();

        //Player W

    }
    IEnumerator pickRight()
    {

        Debug.Log("Right");

        pirateTarget = cup2Rect.localPosition + new Vector3(0f, 20f, 0f);

        yield return new WaitUntil(() => Vector2.Distance(pirateHandRect.localPosition, pirateTarget) == 0f);

        Debug.Log("Arrived. PiratePos: " + pirateHandRect.localPosition + " TargetPos: " + pirateTarget );

        try {
            dice.transform.SetParent(null);
        } catch (Exception e) { 
        
        }

        cup2Target = cup2Rect.localPosition + new Vector3(0f, 70f, 0f);

        pirateTarget = pirateHandRect.localPosition + new Vector3(0f, 70f, 0f);

        yield return new WaitUntil(() => Vector2.Distance(pirateHandRect.localPosition, pirateTarget) == 0f);

        Debug.Log("Arrived. PiratePos: " + pirateHandRect.localPosition + " TargetPos: " + pirateTarget);

        yield return new WaitForSeconds(3);

        OnWin();

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

                switch ((int)UnityEngine.Random.Range(1, 3))
                {

                    case 1:
                        SwitchCups(cup1Rect, cup2Rect, ref cup1Target, ref cup2Target);

                        break;

                    case 2:
                        SwitchCups(cup3Rect, cup2Rect, ref cup3Target, ref cup2Target);

                        break;

                    case 3:
                        SwitchCups(cup1Rect, cup3Rect, ref cup1Target, ref cup3Target);

                        break;

                }

            }

        }

        //move the cups to thier target.

        cup1Rect.localPosition = Vector2.MoveTowards(cup1Rect.localPosition, cup1Target, cupSpeed);

        cup2Rect.localPosition = Vector2.MoveTowards(cup2Rect.localPosition, cup2Target, cupSpeed);

        cup3Rect.localPosition = Vector2.MoveTowards(cup3Rect.localPosition, cup3Target, cupSpeed);

        pirateHandRect.localPosition = Vector2.MoveTowards(pirateHandRect.localPosition, pirateTarget, cupSpeed);

        playerHandRect.localPosition = Vector2.MoveTowards(playerHandRect.localPosition, playerTarget, cupSpeed);

    }

    public void Lift(RectTransform cup, ref Vector2 cupTarget, float height) {

        cupTarget = new Vector2(cup.localPosition.x, cup.localPosition.y + height);

    }

    public void Fall(RectTransform cup, ref Vector2 cupTarget, float height)
    {

        cupTarget = new Vector2(cup.localPosition.x, cup.localPosition.y - height);

    }

    public bool IsAtTarget(RectTransform cup, ref Vector2 cupTarget, float threshold) {
        //checks if the cup has reached its destination 

        if (Vector2.Distance(cup.localPosition, cupTarget) < threshold) {

            return true;

        }

        return false;

    }

    public void SwitchCups(RectTransform cup1, RectTransform cup2, ref Vector2 cup1target, ref Vector2 cup2target) {

        cup1target = cup2.localPosition;

        cup2target = cup1.localPosition;

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

        return IsAtTarget(cup1Rect, ref cup1Target, 0.1f) &&
         IsAtTarget(cup2Rect, ref cup2Target, 0.1f) &&
         IsAtTarget(cup3Rect, ref cup3Target, 0.1f);

        }

}
