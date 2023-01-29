using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject dicePrefab;

    public GameObject cupGamePrefab;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q)){

            Instantiate(dicePrefab);

        }

        if (Input.GetKeyDown(KeyCode.W))
        {

            Instantiate(cupGamePrefab);

        }


    }

}