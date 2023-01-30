using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LockPicking : MonoBehaviour
{

    [System.Serializable]
    internal class RingKeyPair
    {
        public RawImage ring;
        public RawImage key;
        public float angularVelocity = 90f;
    }

    [SerializeField] private List<RingKeyPair> lockKeyPairs;
    [SerializeField] private Slider timeBar;
    [SerializeField] private Image timeBarFillArea;
    [SerializeField] private float totalTime = 10f;
    [SerializeField] private float cooldownTime = 1f;
    [SerializeField] private float angleAllowance = 10f;
    private RingKeyPair current => lockKeyPairs[_currentIndex];
    private int _currentIndex;
    private int currentIndex
    {
        get => _currentIndex;
        set
        {
            if (value >= 0 && value <= lockKeyPairs.Count)
            {
                _currentIndex = value;
                current.key.enabled = true;
            }
            else if (value < 0)
                OnWin();
        }
    }
    private float totalClock;
    private float cooldownClock;
    private bool gameEnded = true;

    private void Start() 
    {
        Initalize();
    }

    private void Update() 
    {
        if (gameEnded)
            return;

        // update clocks
        totalClock = Mathf.Clamp(totalClock - Time.deltaTime, 0, totalTime);
        cooldownClock = Mathf.Clamp(cooldownClock - Time.deltaTime, 0, cooldownTime);
        timeBar.value = totalClock / totalTime;

        if (totalClock == 0)
        {
            OnLoss();
            return;
        }

        Vector3 rotationDelta = Vector3.forward * current.angularVelocity * Time.deltaTime;
        current.key.transform.eulerAngles += rotationDelta;
        if (Input.GetKeyDown(KeyCode.Space) && cooldownClock == 0)
        {
            
            Color temp = current.key.color;
            current.key.color = Color.gray * temp;
            current.key.DOColor(temp, 0.25f);

            if (StopCheck())
            {
                // the player successfully picked the ring
                Vector3 ringRotation = current.ring.transform.eulerAngles;
                current.key.transform.eulerAngles = ringRotation;
                currentIndex--;
            }
            else
            {
                // the player failed to pick the ring
                cooldownClock = cooldownTime;
                // give the player some feedback
                timeBarFillArea.DOColor(Color.red, cooldownTime / 4f).SetLoops(4, LoopType.Yoyo);
                timeBar.transform.DOShakePosition(cooldownTime / 2f);
            }
        }
    }

    private void Initalize()
    {
        //Debug.Log(lockKeyPairs.Count);
        foreach (RingKeyPair lockKeyPair in lockKeyPairs)
        {
            lockKeyPair.key.enabled = false;
            Vector3 rotation = Vector3.forward * Random.Range(0f, 360f);
            lockKeyPair.ring.rectTransform.eulerAngles = rotation;
        }
        currentIndex = lockKeyPairs.Count - 1;
        totalClock = totalTime;
        gameEnded = false;
    }

    private bool StopCheck()
    {
        float angle = Quaternion.Angle(current.key.transform.rotation, current.ring.transform.rotation);
        return Mathf.Abs(angle) <= 10f;
    }

    private void OnWin()
    {
        gameEnded = true;
        Debug.Log("You win!");
    }

    private void OnLoss()
    {
        gameEnded = true;
        Debug.Log("You lost!");
    }
}
