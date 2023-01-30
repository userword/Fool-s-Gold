using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LockPicking : MonoBehaviour
{

    [System.Serializable]
    internal class LockKeyPair
    {
        [SerializeField] private RawImage _ring;
        public RawImage Ring => _ring;
        [SerializeField] private RawImage _key;
        public RawImage Key => _key;
    }

    [SerializeField] private List<LockKeyPair> lockKeyPairs;
    [SerializeField] private float failedCooldown;
    private LockKeyPair current;
    private float cooldownClock;

    private void Start() 
    {
        Init();
    }

    private void Update() 
    {

    }

    private bool StopCheck()
    {
        //float angle = Quaternion.Angle (a, b);
 
        //bool sameRotation = Mathf.Abs (angle) < 1e-3f;
        return true;
    }

    private void Init()
    {
        foreach (LockKeyPair lockKeyPair in lockKeyPairs)
        {
            lockKeyPair.Key.enabled = false;
            Vector3 rotation = Vector3.forward * Random.Range(0f, 360f);
            lockKeyPair.Ring.rectTransform.eulerAngles = rotation;
        }
        StartCoroutine(PickingLoop());
    }

    private IEnumerator PickingLoop()
    {
        for (int i = lockKeyPairs.Count - 1; i >= 0; i--)
        {
            current = lockKeyPairs[i];
            while(current != null)
                yield return new WaitForSeconds(.1f);
        }
        // the player finished picking all the locks!
        Debug.Log("You win!");
        yield return null;
    }
}
