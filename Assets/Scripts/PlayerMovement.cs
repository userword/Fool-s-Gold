using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float movespeed;
    public Scammable chosenScam {get; private set;}
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveDirection;
    private List<Scammable> avalibleScams;
    private GameController GC;

    private void FixedUpdate()
    {
        if (!GameController.frozen) {

            Move();

        }

    }

    private void Awake()
    {
        avalibleScams = new List<Scammable>();

        GC = GameObject.Find("Main Camera").GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
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
