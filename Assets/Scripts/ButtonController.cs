using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer buttonState;
    private SpriteRenderer foolState;
    public GameObject fool;
    public Sprite defaultButton;
    public Sprite pressedButton;
    public Sprite danceMove;
    public Sprite defaultMove;
    public KeyCode buttonPress;
    public AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        buttonState = GetComponent<SpriteRenderer>();
        foolState = fool.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(buttonPress)){
            buttonState.sprite = pressedButton;
            foolState.sprite = danceMove;
            sound.Play();
        }
        if (Input.GetKeyUp(buttonPress)){
            buttonState.sprite = defaultButton;
            foolState.sprite = defaultMove;
            sound.Stop();
        }
    }
}
