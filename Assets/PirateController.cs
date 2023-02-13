using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour
{
    public enum pirateState { 
    
    WANDERING, SEARCHING, CHASING
    
    }
    public enum direction { 
    
    UP, DOWN, LEFT, RIGHT

    }

    private direction movementDirection;

    private pirateState state;

    private float speed = 1;

    Vector3 movement;

    public BuildingColliderHandler leftCollision;

    public BuildingColliderHandler rightCollision;

    public BuildingColliderHandler upperCollision;

    public BuildingColliderHandler lowerCollision;

    public PirateSightHandler sightHandler;

    public Transform sightTransform;


    void Awake()
    {

        movement = new Vector3(0, 0, 0);

        SetDirecton(direction.LEFT);

        state = pirateState.WANDERING;

    }
    void Update()
    {

        if (sightHandler.found) {

            state = pirateState.CHASING;

        } 

        if (state == pirateState.WANDERING) {

            TurnAwayFromWall();

        }

        if (state == pirateState.SEARCHING)
        {

            TurnAwayFromWall();

        }

        if (state == pirateState.CHASING)
        {

            movement = (sightHandler.TargetPos - (Vector2)transform.position).normalized;

            float angle = Vector2.Angle((Vector2)transform.position, sightHandler.TargetPos);

            sightTransform.eulerAngles = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle));

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

    public void TurnAwayFromWall() {

        if (rightCollision.contact == true && movementDirection == direction.RIGHT)
        {

            if (Random.Range(0, 2) == 0) {

                SetDirecton(direction.UP);

            }
            else
            {

                SetDirecton(direction.DOWN);
            }

        }

        if (leftCollision.contact == true && movementDirection == direction.LEFT)
        {

            if (Random.Range(0, 2) == 0)
            {

                SetDirecton(direction.DOWN);

            } else {

                SetDirecton(direction.UP);

            }

        }

        if (upperCollision.contact == true && movementDirection == direction.UP)
        {

            if (Random.Range(0, 2) == 0)
            {

                SetDirecton(direction.LEFT);

            } else {

                SetDirecton(direction.RIGHT);
            }

        }

        if (lowerCollision.contact == true && movementDirection == direction.DOWN)
        {

            if (Random.Range(0, 2) == 0)
            {

                SetDirecton(direction.RIGHT);

            }
            else
            {
                SetDirecton(direction.LEFT);


            }

        }

    }

}
