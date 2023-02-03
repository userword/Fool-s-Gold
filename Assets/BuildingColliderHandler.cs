using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BuildingColliderHandler : MonoBehaviour
{

    public bool contact;

    void Start()
    {
        contact = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
            if (collision.gameObject.TryGetComponent(out Rigidbody2D rb2D)) 
            {

                if (rb2D.bodyType == RigidbodyType2D.Static) { 

                    contact = true;

                    Pause();

                }

            }
        

    }

    IEnumerator Pause(){

       gameObject.GetComponent<BoxCollider2D>().enabled = false;

        contact = false;

       yield return new WaitForSeconds(1.5f);

       gameObject.GetComponent<BoxCollider2D>().enabled = true;

    }


}
