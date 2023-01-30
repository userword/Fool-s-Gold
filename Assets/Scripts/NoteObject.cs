using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject: MonoBehaviour
{
    public bool press;
    public KeyCode buttonPress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(buttonPress)){
            if (press){
                gameObject.SetActive(false);
            }
        }
        
    }
    private void OnTriggerEnter2D (Collider2D other){
        if (other.tag == "Activator"){
            press = true;
        }
    }
    private void OnTriggerExit2D (Collider2D other){
        if (other.tag == "Activator"){
            press = false;
        }
    }
}
