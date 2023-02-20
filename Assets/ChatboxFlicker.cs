using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatboxFlicker : MonoBehaviour
{

    SpriteRenderer sr;

    bool flickering = false;

    private void Awake() {

        sr = gameObject.GetComponent<SpriteRenderer>();

    }

    public IEnumerator Flicker() {

        if (flickering)
        {

            sr.enabled = false;

            yield return new WaitForSeconds(Random.Range(1, 2));

            sr.enabled = true;

        }

        StartCoroutine(Flicker());

    }
    public void Show() {

        sr.enabled = true;

        flickering = true;

    }

    public void Hide() {

        flickering = false;

        sr.enabled = false;

    }


}
