using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LockPicking : MonoBehaviour, MiniGame
{

    [System.Serializable]
    internal class RingKeyPair
    {
        public RawImage ring;
        public RawImage key;
        public float angularVelocity = 90f;
    }

    [SerializeField] private List<RingKeyPair> ringKeyPairs;
    [SerializeField] private Slider timeBar;
    [SerializeField] private Image timeBarFillArea;
    [SerializeField] private float totalTime = 10f;
    [SerializeField] private float cooldownTime = 1f;
    [SerializeField] private float angleAllowance = 10f;

    public GameController gc;
    private RingKeyPair current => ringKeyPairs[_currentIndex];
    private int _currentIndex;
    private int currentIndex
    {
        get => _currentIndex;
        set
        {
            if (value > _currentIndex)
            {
                if (value >= ringKeyPairs.Count)
                    return;
                for (int i = _currentIndex; i < value; i++)
                    ringKeyPairs[i].key.enabled = false;
            }
            else if (value < _currentIndex)
            {
                if (value < 0)
                {
                    OnWin();
                    return;
                }
            }
            _currentIndex = value; // start the next key logically
            current.key.enabled = true;
        }
    }
    private float totalClock;
    private float cooldownClock;
    private bool gameEnded = true;

    private void Start() {
        Initalize(6);
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
                Initalize(false);
                // the player failed to pick the ring
                cooldownClock = cooldownTime;
                // give the player some feedback
                timeBarFillArea.DOColor(Color.red, cooldownTime / 4f).SetLoops(4, LoopType.Yoyo);
                timeBar.transform.DOShakePosition(cooldownTime / 2f);
            }
        }
    }

    public void Initalize(int dieValue)
    {

        gc = GameObject.Find("Main Camera").GetComponent<GameController>();

        float mutliplier = (4 - dieValue) / 6f;

        foreach (RingKeyPair ringKeyPair in ringKeyPairs)
        {

            float velocityDelta = ringKeyPair.angularVelocity * mutliplier;
            ringKeyPair.angularVelocity += velocityDelta;

        }

        Initalize();
    }

    private void Initalize(bool resetTime = true)
    {
        //Debug.Log(lockKeyPairs.Count);
        foreach (RingKeyPair lockKeyPair in ringKeyPairs)
        {
            lockKeyPair.key.enabled = false;
            Vector3 rotation = Vector3.forward * Random.Range(0f, 360f);
            lockKeyPair.ring.rectTransform.eulerAngles = rotation;
        }
        currentIndex = ringKeyPairs.Count - 1;
        if (resetTime) totalClock = totalTime;
        gameEnded = false;
    }

    private bool StopCheck()
    {
        float angle = Quaternion.Angle(current.key.transform.rotation, current.ring.transform.rotation);
        return Mathf.Abs(angle) <= 10f;
    }

    public void OnWin()
    {
        gameEnded = true;
        GameManager.Singleton.OnWin();
        Destroy(gameObject.transform.root.gameObject);
        Destroy(gc.dicePrefabRef);
    }

    public void OnLoss()
    {
        gameEnded = true;
        GameManager.Singleton.OnLoss();
        Destroy(gameObject.transform.root.gameObject);
        Destroy(gc.dicePrefabRef);
    }
}
