using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupGame : MonoBehaviour
{

    public GameObject bar;

    private RectTransform barRect;

    public GameObject redBar; //Outer

    private RectTransform redBarRect;

    public GameObject yellowBar;

    private RectTransform yellowBarRect;

    public GameObject greenBar; //Center

    private RectTransform greenBarRect;

    float leftBound, rightBound;

    bool forwards = false, backwards = false, start = false;

    private float barspeed = 100f;

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

        greenBarRect = greenBar.GetComponent<RectTransform>();

        yellowBarRect = yellowBar.GetComponent<RectTransform>();

        redBarRect = redBar.GetComponent<RectTransform>();

        //set bounds for moving black bar

        leftBound = redBarRect.localPosition.x - redBarRect.rect.width/2;

        rightBound = redBarRect.localPosition.x + redBarRect.rect.width / 2;

        //Debug.Log("left: " + leftBound + "\n" + "right: " + rightBound + "\n");

    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.S))
        {

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

            Debug.Log("Cup positions: \n" + cup1.transform.position + " " + cup2.transform.position + " " + cup3.transform.position);

            Debug.Log("Cup targets: \n" + cup1Target + " " + cup2Target + " " + cup3Target);

        }

        if (Input.GetKeyDown(KeyCode.A))
        {

            start = true;

            forwards = true;

        }

    }
    void FixedUpdate()
    {

        float barX = barRect.localPosition.x;

        //Debug.Log("bar: " + barX);

        if (forwards && start)
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

        if (start)
        {

          cup1.transform.position = Vector2.MoveTowards(cup1.transform.position, cup1Target, 5f);

            cup2.transform.position = Vector2.MoveTowards(cup2.transform.position, cup2Target, 5f);

            cup3.transform.position = Vector2.MoveTowards(cup3.transform.position, cup3Target, 5f);

        }

    }

    public void SwitchCups(GameObject cup1, GameObject cup2, ref Vector2 cup1target, ref Vector2 cup2target) {

        cup1target = cup2.transform.position;

        cup2target = cup1.transform.position;

    }

}
