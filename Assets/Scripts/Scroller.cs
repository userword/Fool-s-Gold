using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    public float BPM;
    public bool isStart = false;
    // Start is called before the first frame update
    void Start()
    {
        BPM = BPM / 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if ((isStart)){
            transform.position -= new Vector3 (0f, BPM * Time.deltaTime, 0); 
    }
}
}
