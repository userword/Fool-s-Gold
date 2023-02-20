using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSpawn : MonoBehaviour
{
    //spawn rate, Chest pirate frequency, 

    public GameObject piratePrefab;

    int waittime, waitrange;

    IEnumerator SpawnPirate() {

        Instantiate(piratePrefab);
    
        
    
    }


}
