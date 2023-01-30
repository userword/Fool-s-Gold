using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowBarCollisionHandler : MonoBehaviour
{
    bool yellow;

    private void Awake()
    {
        yellow = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Just collided with " + collision.gameObject.name);

        yellow = true;

    }

    void OnTriggerExit2D(Collider2D collision)
    {

        Debug.Log("Just exited with " + collision.gameObject.name);

        yellow = false;
 
    }

    public bool Status()
    {

        return yellow;

    }

}
