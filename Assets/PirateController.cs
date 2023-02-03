using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour
{

    public enum direction { 
    
    UP, DOWN, LEFT, RIGHT

    }

    private direction movementDirection;

    private float speed = 1;

    Vector3 movement;

    public BuildingColliderHandler leftCollision;

    public BuildingColliderHandler rightCollision;

    public BuildingColliderHandler upperCollision;

    public BuildingColliderHandler lowerCollision;

    public Transform sightTransform;


    void Awake()
    {

        movement = new Vector3(0, 0, 0);

        SetDirecton(direction.LEFT);

    }
    void Update()
    {

        if (rightCollision.contact == true && movementDirection == direction.RIGHT) {

            SetDirecton(direction.UP);

        }

        if (leftCollision.contact == true && movementDirection == direction.LEFT) {

            SetDirecton(direction.DOWN);

        }

        if (upperCollision.contact == true && movementDirection == direction.UP)
        {

            SetDirecton(direction.LEFT);

        }

        if (lowerCollision.contact == true && movementDirection == direction.DOWN)
        {

            SetDirecton(direction.RIGHT);

        }

    }

    private void FixedUpdate()
    {

        transform.Translate(movement * speed * Time.deltaTime);

    }

    public void SetDirecton(direction newDirection) {

        movementDirection = newDirection;

        switch (newDirection) {

            case direction.UP:

                movement = new Vector3(0, 1, 0);

                sightTransform.eulerAngles = new Vector3(0f, 0f, 90f);

                break;

            case direction.DOWN:

                movement = new Vector3(0, -1, 0);

                sightTransform.eulerAngles = new Vector3(0f, 0f, 270f);

                break;

            case direction.LEFT:

                movement = new Vector3(-1, 0, 0);

                sightTransform.eulerAngles = new Vector3(0f, 0f, 1800f);

                break;

            case direction.RIGHT:

                sightTransform.eulerAngles = new Vector3(0f, 0f, 0f);

                movement = new Vector3(1, 0, 0);

                break;

        }

    }

}
