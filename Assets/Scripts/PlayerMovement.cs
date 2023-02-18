using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;

    public float movespeed;

    Vector2 moveDirection;

    public Animator animator;

    private List<Scammable> avalibleScams;

    Scammable chosenScam;

    private void FixedUpdate()
    {

        Move();
    }

    private void Awake()
    {

    }
    private void Update()
    {

        HandleSelectionInputs();

        animator.SetFloat("Movespeed", rb.velocity.sqrMagnitude);

    }
    private void HandleSelectionInputs()
    {

        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY);

    }
    private void Move()
    {

        rb.velocity = new Vector2(moveDirection.x * movespeed, moveDirection.y * movespeed);
        
    }

    public void ExecuteScam()
    {

        chosenScam.Go();

    }

    public void ChooseClosestScam() {

        foreach (Scammable scam in avalibleScams)
        {

            if (scam.distanceToPlayer <= chosenScam.distanceToPlayer)
            {

                chosenScam = scam;

            }

        }

    }

    public void RegisterScam(Scammable scam)
    {

        avalibleScams.Add(scam);

    }

    public void DeclineScam(Scammable scam)
    {

        avalibleScams.Remove(scam);

    }


}
