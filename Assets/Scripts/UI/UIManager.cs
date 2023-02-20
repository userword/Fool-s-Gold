using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager _singleton;
    public static UIManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(UIManager)} instance already exists, destroying object!");
                Destroy(value);
            }
        }
    }

    private void Awake() {
        Singleton = this;
    }

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsMenu;

    private GameObject _activeMenu;
    private GameObject activeMenu 
    {
        get => _activeMenu;
        set
        {
            mainMenu.SetActive(false);
            creditsMenu.SetActive(false);
            // maybe add some sort of an effect here
            value.SetActive(true);
            _activeMenu = value;
        }
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void OnCreditsButton()
    {
        activeMenu = creditsMenu;
    }

    public void OnAnyKeyCredits()
    {
        activeMenu = mainMenu;
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}
