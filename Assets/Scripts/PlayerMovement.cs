using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float movespeed;
    [SerializeField] private float boostMutliplier = 1.5f;
    [SerializeField] private float boostDuration = 2f;
    [SerializeField] private float boostRechargeSpeed = 0.5f;
    [SerializeField] private float boostRechargeDelay = 2f;
    public Scammable chosenScam {get; private set;}
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 moveDirection;
    private List<Scammable> avalibleScams;
    private GameController GC;
    private bool isBoostActive;
    private float _boostClock;
    private float boostClock 
    {
        get => _boostClock;
        set
        {
            _boostClock = value;
            GameMenu.Singleton.UpdateBoostBar(boostClock / boostDuration);
        }
    }
    private float boostRechargeClock;

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

        boostClock = boostDuration;
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

        if (moveDirection != Vector2.zero && Input.GetKey(KeyCode.LeftShift) && boostClock > 0)
        {
            isBoostActive = true;
            float boostClockDelta = -Time.deltaTime;
            boostClock = Mathf.Clamp(boostClock + boostClockDelta, 0, boostDuration);
            if (boostClock == 0) boostRechargeClock = boostRechargeDelay;
        }
        else
        {
            isBoostActive = false;
            float rechargeClockDelta = -Time.deltaTime;
            boostRechargeClock = Mathf.Clamp(boostRechargeClock + rechargeClockDelta, 0, boostRechargeDelay);

            if (boostRechargeClock == 0)
            {
                float boostClockDelta = boostRechargeSpeed * Time.deltaTime;
                boostClock = Mathf.Clamp(boostClock + boostClockDelta, 0, boostDuration);
            }
        }

    }
    private void Move()
    {
        float speed = movespeed * (isBoostActive ? boostMutliplier : 1f);
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
        
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
