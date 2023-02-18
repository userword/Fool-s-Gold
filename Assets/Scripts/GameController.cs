using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public bool frozen = false;

    public int currentRoll;

    public GameObject dicePrefab;

    public GameObject dicePrefabRef;

    public GameObject cupGamePrefab;

    public GameObject cupGame;

    public GameObject sweetTalkingMinigamePrefab;

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q)) {

            dicePrefabRef = Instantiate(dicePrefab);

            dicePrefabRef.transform.parent = this.transform;

        }

        if (Input.GetKeyDown(KeyCode.W))
        {

            StartCoroutine(PlayCupGame());

        }

        if (Input.GetKeyDown(KeyCode.E))
        {

            Instantiate(sweetTalkingMinigamePrefab);

        }

    }

    public void RollDice() {

        dicePrefabRef = Instantiate(dicePrefab);

        dicePrefabRef.transform.parent = this.transform;

        dicePrefabRef.GetComponent<DiceShooter>().Play();

    }

    public IEnumerator PlayCupGame() {

        RollDice();

        yield return new WaitUntil(() => dicePrefabRef.GetComponentInChildren<DiceRoller>().final != 7);

        cupGame = Instantiate(cupGamePrefab);

        currentRoll = dicePrefabRef.GetComponentInChildren<DiceRoller>().final;

        cupGame.GetComponent<CupGame>().Initalize(currentRoll);

    }
    public void PlayLockpickingGame() { }
    public void PlaySweetTalkinGame() { }
    public void PlayPickpocketingGame() { }


}