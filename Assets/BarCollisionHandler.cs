using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarCollisionHandler : MonoBehaviour
{

    //I'm an idiot
    //I wrote this class and then rewrote it because I thought it didnt work
    //turns out I just didn't have the colliders set correctly...

    bool green, yellow, red;

    private void Awake()
    {

        green = false;
        red = false;
        yellow = false;
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Debug.Log("Just collided with " + collision.gameObject.name);

        switch (collision.gameObject.name) {

            case "Green":
                green = true;
                break;

            case "Yellow":
                yellow = true;
                break;

            case "Red":
                red = true;
                break;

        }

    }

    void OnTriggerExit2D(Collider2D collision)
    {

       // Debug.Log("Just exited with " + collision.gameObject.name);

        switch (collision.gameObject.name)
        {

            case "Green":
                green = false;
                break;

            case "Yellow":
                yellow = false;
                break;

            case "Red":
                red = false;
                break;

        }

    }

    public string Status() {

        if (green) { return "green"; }

        if (yellow) { return "yellow"; }

        if (red) { return "red"; }

        return "no collision";

    }

}
