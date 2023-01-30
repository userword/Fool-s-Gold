using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBarCollisionHandler : MonoBehaviour
{

    bool green;

    private void Awake()
    {
        green = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        Debug.Log("Just collided with " + collision.gameObject.name);

        green = true;

    }

    void OnTriggerExit2D(Collider2D collision)
    {

        Debug.Log("Just exited with " + collision.gameObject.name);

        green = false;

    }

    public bool Status()
    {

        return green;

    }
}
