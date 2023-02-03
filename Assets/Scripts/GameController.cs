using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public bool frozen = false;

    public GameObject dicePrefab;
    public GameObject dicePrefabRef;

    public GameObject cupGamePrefab;

    public GameObject sweetTalkingMinigamePrefab;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q)){

            dicePrefabRef = Instantiate(dicePrefab);

            dicePrefabRef.transform.parent = this.transform;

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