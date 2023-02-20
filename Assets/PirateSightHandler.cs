using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSightHandler : MonoBehaviour
{

    public Vector2 TargetPos;

    public Transform targetTransform;

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

            TargetPos = collision.gameObject.transform.position;

            targetTransform = collision.gameObject.transform;

            found = true;

        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.gameObject.name == "PlayerParent")
        {

            found = false;

        }

    }
}
