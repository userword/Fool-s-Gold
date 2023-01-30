using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupGame : MonoBehaviour
{

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

    bool forwards = false, backwards = false, 
        shuffling = false, start2 = false; // these booleans are supposed to track the different Mini game states

    private float barspeed = 100f;

    private float cupSpeed = 10f;

    public GameObject cup1, cup2, cup3;

    private Vector2 cup1Target, cup2Target, cup3Target;

    void Awake()
    {

        //keep cups in place

        cup1Target = cup1.transform.position;

        cup2Target = cup2.transform.position;

        cup3Target = cup3.transform.position;

        Debug.Log("Cup positions: \n" + cup1.transform.position + " " + cup2.transform.position + " " + cup3.transform.position);

        Debug.Log("Cup targets: \n" + cup1Target + " " + cup2Target + " " + cup3Target);

        //get components

        barRect = bar.GetComponent<RectTransform>();

        barCollisionHandler = bar.GetComponent<BarCollisionHandler>();

        greenBarRect = greenBar.GetComponent<RectTransform>();

        greenBarCollisionHandler = greenBar.GetComponent<GreenBarCollisionHandler>();

        yellowBarRect = yellowBar.GetComponent<RectTransform>();

        yellowBarCollisionHandler = yellowBar.GetComponent<YellowBarCollisionHandler>();

        redBarRect = redBar.GetComponent<RectTransform>();

        //set bounds for moving black bar

        leftBound = redBarRect.localPosition.x - redBarRect.rect.width/2;

        rightBound = redBarRect.localPosition.x + redBarRect.rect.width / 2;

        //Debug.Log("left: " + leftBound + "\n" + "right: " + rightBound + "\n");

    }

    IEnumerator ShowDice() {

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

            StartCoroutine(ShowDice());

        }

        if (Input.GetKeyDown(KeyCode.A)) // Stop bar
        {

            backwards = false;
            forwards = false;

            Debug.Log(Status());

        }

    }

    void FixedUpdate()
    {

        float barX = barRect.localPosition.x;

        //Debug.Log("bar: " + barX);

        if (forwards && shuffling)
        {

            bar.transform.Translate(new Vector2(1f, 0f) * barspeed * Time.deltaTime);

        } else if (backwards){
       
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
            if (IsAtTarget(cup1, ref cup1Target, 0.1f) &&
                IsAtTarget(cup2, ref cup2Target, 0.1f) &&
                IsAtTarget(cup3, ref cup3Target, 0.1f))
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

}
