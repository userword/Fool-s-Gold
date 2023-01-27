using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Vector2 mousePosition;

    private float mouseX, mouseY, speed = 3f;

    private void Update()
    {

        mousePosition = Input.mousePosition;

        mouseX = mousePosition.x - Screen.width / 2;

        mouseY = mousePosition.y - Screen.height / 2;


        HandleSelectionInputs();

    }
    private void HandleSelectionInputs()
    {

        if (Input.GetKey(KeyCode.Mouse0))
        {

            Vector3 movement = Vector3.Normalize(new Vector3(mouseX, mouseY, 0));

            transform.Translate(movement * speed * Time.deltaTime);

        }

    }

}
