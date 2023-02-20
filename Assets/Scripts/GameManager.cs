using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _singleton;
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
    }

    public void OnWin()
    {
        Debug.Log("player won a game!");
    }

    public void OnLoss()
    {
        Debug.Log("player lost a game!");
    }
    
    private void Start()
    {
        //Instantiate(diceRollPrefab);
    }
}
