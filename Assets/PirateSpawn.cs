using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateSpawn : MonoBehaviour
{
    //spawn rate, Chest pirate frequency, 

    public GameObject piratePrefab;

    int waittime, waitrange;

    public IEnumerator SpawnPirate() {

        GameObject pirate = Instantiate(piratePrefab);

        PirateController pc = pirate.GetComponent<PirateController>();


       yield return new WaitForSeconds(1);
    
    }


}
