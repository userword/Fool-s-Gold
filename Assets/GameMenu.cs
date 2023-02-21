using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMenu : MonoBehaviour
{
    private static GameMenu _singleton;
    public static GameMenu Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(GameMenu)} instance already exists, destroying object!");
                Destroy(value);
            }
        }
    }

    [SerializeField] private Slider BoostBar;

    private void Awake() 
    {
        Singleton = this;
    }

    public void UpdateBoostBar(float amount)
    {
        BoostBar.value = Mathf.Clamp01(amount);
    }
}
