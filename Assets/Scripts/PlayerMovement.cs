using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    GameController GC;

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
        avalibleScams = new List<Scammable>();

        GC = GameObject.Find("Main Camera").GetComponent<GameController>();

    }
    private void Update()
    {

        HandleSelectionInputs();



        animator.SetFloat("Movespeed", rb.velocity.sqrMagnitude);

    }
    private void HandleSelectionInputs()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (avalibleScams.Count > 0 && !GameController.frozen) {

                GameController.frozen = true;

                ChooseClosestScam();
                ExecuteScam();

            }

        }

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

        chosenScam = avalibleScams[0];

        foreach (Scammable scam in avalibleScams)
        {

            if (scam.distanceToPlayer <= chosenScam.distanceToPlayer && chosenScam)
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
