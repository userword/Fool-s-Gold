using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public GameObject dicePrefab;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)){

            Instantiate(dicePrefab);

        }

    }

}