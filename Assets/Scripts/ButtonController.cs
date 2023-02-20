using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer buttonState;
    public Sprite defaultButton;
    public Sprite pressedButton;
    public KeyCode buttonPress;
    public AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        buttonState = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(buttonPress)){
            buttonState.sprite = pressedButton;
            sound.Play();
        }
        if (Input.GetKeyUp(buttonPress)){
            buttonState.sprite = defaultButton;
            sound.Stop();
        }
    }
}
