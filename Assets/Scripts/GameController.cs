using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject dicePrefab;

    public GameObject cupGamePrefab;

    public GameObject sweetTalkingMinigamePrefab;


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q)){

            Instantiate(dicePrefab);

        }

        if (Input.GetKeyDown(KeyCode.W))
        {

            Instantiate(cupGamePrefab);

        }

        if (Input.GetKeyDown(KeyCode.E))
        {

            Instantiate(sweetTalkingMinigamePrefab);

        }


    }

}