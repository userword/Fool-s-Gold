using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSightHandler : MonoBehaviour
{

    public Vector2 TargetPos;

    public bool found;
    void Start()
    {
        TargetPos = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.name == "PlayerParent")
        {

            TargetPos = gameObject.transform.position;

            found = true;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.name == "PlayerParent")
        {

            TargetPos = Vector2.zero;

            found = false;

        }

    }
}
