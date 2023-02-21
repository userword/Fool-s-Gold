using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RumSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject RumPrefab;

    public GameObject Rum;

    public float waittime, waitrange;

    private void Start()
    {

        StartCoroutine(SpawnRum());

    }
    public IEnumerator SpawnRum()
    {

        int time = (int)Random.Range(waittime, waittime + waitrange);

        yield return new WaitForSeconds(time);

        GameObject pirate = Instantiate(Rum);

        pirate.transform.position = this.transform.position;

        StartCoroutine(SpawnRum());

    }


}
