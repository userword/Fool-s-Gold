using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _singleton;

   public GameController gc;

    public int score = 0;
    public TMPro.TextMeshProUGUI scoreCounterText;

    public static GameManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(GameManager)} instance already exists, destroying object!");
                Destroy(value);
            }
        }
    }

    [SerializeField] private GameObject diceRollPrefab;
    [SerializeField] private GameObject lockPickingPrefab;

    private void Awake() {

        Singleton = this;

        gc = GameObject.Find("Main Camera").GetComponent<GameController>();

        scoreCounterText = GameObject.Find("ScoreText").GetComponentInChildren<TMPro.TextMeshProUGUI>();

    }

    public void OnWin()
    {

        GameController.frozen = false;

        score += 10 * (7 - gc.currentRoll);

        Debug.Log(score);

        print(gc.currentRoll);

        scoreCounterText.text = score.ToString();
        Debug.Log("player won a game!");

    }

    public void OnLoss()
    {

        GameController.frozen = false;

        Debug.Log("player lost a game!");
    }
    
    private void Start()
    {
        //Instantiate(diceRollPrefab);
    }
}
