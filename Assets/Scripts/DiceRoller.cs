using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceRoller : MonoBehaviour
{

    int current;

    public int final = 7; 

    int numBounces = 0;

    public Sprite D1, D2, D3, D4, D5, D6;

    SpriteRenderer spriteRenderer;

    Rigidbody2D rb;

    Vector3 EndPos;
    Quaternion EndRotaion = new Quaternion(0f, 0f, 0f, 0f);

    void Awake() {

        current = RollD6();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        rb = this.GetComponent<Rigidbody2D>();

        EndPos = new Vector3(0, 4f, 0);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

       // if (collision.gameObject.name == "Floor")
            

            if (numBounces < 6)
            {

                current = RollD6Exclusive(current);

                numBounces++;


            } else {

                rb.simulated = false;
            
            }
        

    }

    public int RollD6() {

        return (int)Random.Range(1, 6);
    
    }

    public int RollD6Exclusive(int current) {

        int next = RollD6();

        if (next == current)
        {

            return RollD6Exclusive(next);

        }

        return next;
    
    }

    void Update()
    {
        switch (current) {

            case 1:
                spriteRenderer.sprite = D1;
                break;

            case 2:
                spriteRenderer.sprite = D2;
                break;

            case 3:
                spriteRenderer.sprite = D3;
                break;

            case 4:
                spriteRenderer.sprite = D4;
                break;

            case 5:
                spriteRenderer.sprite = D5;
                break;

            case 6:
                spriteRenderer.sprite = D6;
                break;

        }

        if (rb.velocity.magnitude <= 0.5f) {

            rb.velocity = new Vector2(0, 0);

            rb.angularVelocity = 0;

        }

        if (!rb.simulated) {

            transform.localPosition = Vector2.MoveTowards(transform.localPosition, EndPos, 0.01f);

            transform.rotation = new Quaternion(0f, 0f, 0f, 0f);

            final = current;

        }

        if (Vector2.Distance(transform.localPosition, EndPos) < 0.1f) {



            //Debug.Log("Rolled a" + final);

        }

    }

}
