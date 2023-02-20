using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateController : MonoBehaviour
{
    public enum pirateState { 
    
    WANDERING, SEARCHING, CHASING, CHATTING, DRUNK, CARRYING
    
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

        SetDirecton(direction.UP);

        state = pirateState.WANDERING;

    }
    void Update()
    {

        switch (state)
        {

            case pirateState.WANDERING:
                TurnAwayFromWall();

                break;

            case pirateState.SEARCHING:

                TurnAwayFromWall();

                if (sightHandler.found)
                {
                    state = pirateState.CHASING;
                }

                break;

            case pirateState.CHASING:

                movement = (sightHandler.TargetPos - (Vector2)transform.position).normalized;

                sightTransform.right = sightHandler.TargetPos - (Vector2)transform.position;

                break;

            case pirateState.CHATTING:


                break;

            case pirateState.DRUNK:


                break;

            case pirateState.CARRYING:


                break;

        }

        

        if (state == pirateState.WANDERING) {

            

        }

        if (state == pirateState.SEARCHING)
        {

            TurnAwayFromWall();

        }

        if (state == pirateState.CHASING)
        {



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

                sightTransform.eulerAngles = new Vector3(0f, 0f, 180f);

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
