using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    public static bool frozen = false;

    public int currentRoll;

    public GameObject dicePrefab;

    public GameObject dicePrefabRef;

    public GameObject miniGame; //used to store the current minigame

    public GameObject cupGamePrefab;

    public GameObject pickpocketingMinigamePrefab;

    public GameObject sweetTalkingMinigamePrefab;

    public GameObject lockpickingMinigamePrefab;

    void Update() { }

    public void RollDice() {

        dicePrefabRef = Instantiate(dicePrefab);

        dicePrefabRef.transform.parent = this.transform;

        dicePrefabRef.transform.position = this.transform.position + new Vector3(0f, 0f, 10f);

        dicePrefabRef.GetComponent<DiceShooter>().Play();

    }

    public IEnumerator PlayCupGame() {

        frozen = true;

        RollDice();

        yield return new WaitUntil(() => dicePrefabRef.GetComponentInChildren<DiceRoller>().final != 7);

        currentRoll = dicePrefabRef.GetComponentInChildren<DiceRoller>().final;

        miniGame = Instantiate(cupGamePrefab);

        miniGame.GetComponent<CupGame>().Initalize(currentRoll);

    }
    public IEnumerator PlayLockpickingGame()
    {

        frozen = true;

        RollDice();

        yield return new WaitUntil(() => dicePrefabRef.GetComponentInChildren<DiceRoller>().final < 7);

        currentRoll = dicePrefabRef.GetComponentInChildren<DiceRoller>().final;

        miniGame = Instantiate(lockpickingMinigamePrefab);

        miniGame.GetComponentInChildren<LockPicking>().Initalize(currentRoll);

    }

    public IEnumerator PlaySweetTalkingGame() 
    {
        frozen = true;

        RollDice();

        yield return new WaitUntil(() => dicePrefabRef.GetComponentInChildren<DiceRoller>().final != 7);

        currentRoll = dicePrefabRef.GetComponentInChildren<DiceRoller>().final;

        miniGame = Instantiate(sweetTalkingMinigamePrefab);

        miniGame.GetComponentInChildren<SweetTalking>().Initalize(currentRoll);

    }


    public void PlayPickpocketingGame() {

        frozen = true;

        RollDice();

        Debug.Log("Waiting");

        //yield return new WaitUntil(() => dicePrefabRef.GetComponentInChildren<DiceRoller>().final != 7);

        //yield return new WaitForSecondsRealtime(2);

        Debug.Log("Done");

        StartCoroutine(PlayPickpocketingGameP());

    }
    public IEnumerator PlayPickpocketingGameP() {

        yield return new WaitUntil(() => dicePrefabRef.GetComponentInChildren<DiceRoller>().final != 7);

        currentRoll = dicePrefabRef.GetComponentInChildren<DiceRoller>().final;

        miniGame = Instantiate(pickpocketingMinigamePrefab);

        miniGame.GetComponentInChildren<PickpocketScript>().Initialize(currentRoll);

    }


}