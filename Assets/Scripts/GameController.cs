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

    public GameObject sweetTalkingMinigamePrefab;

    public GameObject lockpickingMinigamePrefab;

    void Update()
    {



    }

    public void RollDice() {

        dicePrefabRef = Instantiate(dicePrefab);

        dicePrefabRef.transform.parent = this.transform;

        dicePrefabRef.GetComponent<DiceShooter>().Play();

    }

    public IEnumerator PlayCupGame() {

        RollDice();

        yield return new WaitUntil(() => dicePrefabRef.GetComponentInChildren<DiceRoller>().final != 7);

        miniGame = Instantiate(cupGamePrefab);

        currentRoll = dicePrefabRef.GetComponentInChildren<DiceRoller>().final;

        miniGame.GetComponent<CupGame>().Initalize(currentRoll);

    }
    public IEnumerator PlayLockpickingGame()
    {

        RollDice();

        yield return new WaitUntil(() => dicePrefabRef.GetComponentInChildren<DiceRoller>().final != 7);

        miniGame = Instantiate(lockpickingMinigamePrefab);

        currentRoll = dicePrefabRef.GetComponentInChildren<DiceRoller>().final;

        miniGame.GetComponentInChildren<LockPicking>().Initalize(currentRoll);

    }

    public void PlaySweetTalkinGame() { }
    public void PlayPickpocketingGame() { }


}