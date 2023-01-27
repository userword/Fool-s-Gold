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

    bool forward;

    private float barspeed = 100f;

    void Awake()
    {
        forward = true;

        barRect = bar.GetComponent<RectTransform>();

        greenBarRect = greenBar.GetComponent<RectTransform>();

        yellowBarRect = yellowBar.GetComponent<RectTransform>();

        redBarRect = redBar.GetComponent<RectTransform>();

        leftBound = redBarRect.localPosition.x - redBarRect.rect.width/2;

        rightBound = redBarRect.localPosition.x + redBarRect.rect.width / 2;

        Debug.Log("left: " + leftBound + "\n" + "right: " + rightBound + "\n");

    }

    void FixedUpdate()
    {

        float barX = barRect.localPosition.x;

        Debug.Log("bar: " + barX);

        if (forward)
        {

            bar.transform.Translate(new Vector2(1f, 0f) * barspeed * Time.deltaTime);

        } else {
       
            bar.transform.Translate(new Vector2(-1f, 0f) * barspeed * Time.deltaTime);

        }

        if (barX < leftBound) {

            forward = true;

        }

        if (barX > rightBound)
        {

            forward = false;

        }

        Debug.Log("left: " + leftBound + "\n" + "bar: " + barX + "\n" + "right: " + rightBound + "\n" + "forward?: " + forward);

    }

}
