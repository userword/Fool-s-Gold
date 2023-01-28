using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceShooter : MonoBehaviour
{
    public GameObject Dice;

    private Rigidbody2D diceRB;

    public GameObject Floor;

    private BoxCollider2D floorBC;

    private void Awake()
    {
        Dice.SetActive(true);

        diceRB = Dice.GetComponent<Rigidbody2D>();

        floorBC = Floor.GetComponent<BoxCollider2D>();


        diceRB.AddForce(new Vector2(150f, 0));

    }

    public int Shoot()
    {//rewrite idk... 

        Dice.SetActive(true);

        diceRB.AddForce(new Vector2(150f, 0));

        return 1;

    }

    void Update()
    {
        
    }

}