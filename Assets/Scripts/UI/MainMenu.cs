using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private static MainMenu _singleton;
    public static MainMenu Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(MainMenu)} instance already exists, destroying object!");
                Destroy(value);
            }
        }
    }

    private void Awake() {
        Singleton = this;
    }

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject creditsMenu;
    [SerializeField] private GameObject loadingMenu;

    private GameObject _activeMenu;
    private GameObject activeMenu 
    {
        get => _activeMenu;
        set
        {
            mainMenu.SetActive(false);
            creditsMenu.SetActive(false);
            loadingMenu.SetActive(false);
            // maybe add some sort of an effect here
            value.SetActive(true);
            _activeMenu = value;
        }
    }

    public void OnPlayButton()
    {
        activeMenu = loadingMenu;
        StartCoroutine("SceneLoad");
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

    private IEnumerator SceneLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("Main Game");
        operation.allowSceneActivation = false;
        yield return new WaitForSeconds(1f);
        operation.allowSceneActivation = true;
    }
}
