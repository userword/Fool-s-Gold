using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    [SerializeField] private Slider boostBar;
    [SerializeField] private float hideBarDelay = 4f; // if the UpdateBoostBar method has not been called recently
    private float hideBarClock;
    private Vector3 boostBarScale;
    private bool boostBarHidden;
    private void Awake() 
    {
        Singleton = this;
    }

    private void Start()
    {
        hideBarClock = 0f;
        boostBarHidden = true;
        boostBarScale = boostBar.transform.localScale;
        boostBar.transform.localScale = Vector3.zero;
    }

    public void UpdateBoostBar(float amount)
    {
        boostBar.value = Mathf.Clamp01(amount);
        hideBarClock = hideBarDelay;
    }

    private void Update()
    {
        float hideBarClockDelta = -Time.deltaTime;
        hideBarClock = Mathf.Clamp(hideBarClock + hideBarClockDelta, 0, hideBarDelay);

        if (hideBarClock == 0 && !boostBarHidden)
        {
            boostBar.transform.DOScale(Vector3.zero, 0.5f);
            boostBarHidden = true;
        }
        else if (hideBarClock != 0 && boostBarHidden)
        {
            boostBar.transform.DOScale(boostBarScale, 0.5f);
            boostBarHidden = false;
        }
    }
}
