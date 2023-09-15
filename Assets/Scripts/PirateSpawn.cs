using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PirateSpawn : MonoBehaviour
{
    //spawn rate, Chest pirate frequency, 

    public GameObject piratePrefab;

    public float waittime, waitrange;

    public PirateController.direction defaultDirection;

    private void Start()
    {

        StartCoroutine(SpawnPirate());

    }
    public IEnumerator SpawnPirate() {

        int time = (int)Random.Range(waittime, waittime + waitrange);

        yield return new WaitForSeconds(time);

        GameObject pirate = Instantiate(piratePrefab);

        pirate.transform.position = this.transform.position;

        PirateController pc = pirate.GetComponent<PirateController>();

        int chestChance = (int)Random.Range(0, 10);

        if (chestChance > 5) {

            pc.myChest.SetActive(true);

        }

        pc.SetMode(PirateController.pirateState.WANDERING);

        pc.SetDirecton(defaultDirection);

        StartCoroutine(SpawnPirate());
    
    }


}
