using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CupGame : MonoBehaviour
{

    public GameObject bar;

    public GameObject redBar; //Outer

    private Image redBarImage;

    public GameObject yellowBar;

    private Image yellowBarImage;

    public GameObject greenBar; //Center

    private Image greenBarImage;

    float leftBound, rightBound;

    bool forward = true;

    private float barspeed = 0.5f;

    void Awake()
    {

        greenBarImage = greenBar.GetComponent<Image>();

        yellowBarImage = yellowBar.GetComponent<Image>();

        redBarImage = redBar.GetComponent<Image>();

        //leftBound = redBarCorners[0].x;

        //rightBound = redBarCorners[2].x;

        Debug.Log("right bound: " + rightBound);

        Debug.Log("bar: " + bar.GetComponent<Image>().rectTransform.rect.x);

    }
    void Update()
    {

        float barX = 0f;

        if (forward && barX > leftBound)
        {

            bar.transform.Translate(new Vector2(1f, 0f) * barspeed * Time.time);

        }
        else if (forward && barX >= rightBound) 
        {
            forward = false;
        }

        if (!forward && barX < rightBound) {

            bar.transform.Translate(new Vector2(-1f, 0f) * barspeed * Time.time);

        } else if (!forward && barX <= leftBound) 
        {
            forward = true; 
        }

    }

}
