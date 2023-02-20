using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreditsScroll : MonoBehaviour
{
    [SerializeField] private RectTransform topRect;
    [SerializeField] private RectTransform bottomRect;
    [SerializeField] private GameObject creditPrefab;
    [SerializeField] private float speed = 50f;
    [SerializeField] private float percentTillNext = 0.25f;
    [SerializeField] private int alphaFrequency = 2;
    [SerializeField] private List<string> credits;
    private Transform[] creditTransforms;
    private Dictionary<Transform, TextMeshProUGUI> creditTextObjects;
    private List<CreditData> movingCredits;
    private int transformPointer;

    internal class CreditData
    {
        public Transform transform;
        public bool hasAdded;

        public CreditData(Transform transform)
        {
            this.transform = transform;
        }
    }

    private void Awake() {
        creditTransforms = new Transform[credits.Count];
        creditTextObjects = new Dictionary<Transform, TextMeshProUGUI>();
        movingCredits = new List<CreditData>();
        for (int i = 0; i < credits.Count; i++)
        {
            string credit = credits[i];
            GameObject gameObject = Instantiate(creditPrefab, transform);
            gameObject.GetComponent<TextMeshProUGUI>().text = credit;
            gameObject.SetActive(false);
            creditTransforms[i] = gameObject.transform;
            TextMeshProUGUI textObject = gameObject.GetComponent<TextMeshProUGUI>();
            creditTextObjects.Add(gameObject.transform, textObject);
        }
    }

    private void OnEnable() {
        movingCredits.Clear();
        foreach (Transform creditTransform in creditTransforms)
        {
            creditTransform.position = topRect.transform.position;
            creditTransform.gameObject.SetActive(false);
        }
        AddNext();
    }

    private void AddNext()
    {
        Transform creditTransform = creditTransforms[transformPointer];
        creditTransform.gameObject.SetActive(true);
        movingCredits.Add(new CreditData(creditTransform));
        transformPointer = (transformPointer + 1) % creditTransforms.Length;
        //Debug.Log(transformPointer);
    }

    void Update()
    {
        for (int i = movingCredits.Count - 1; i >= 0; i--)
        {
            CreditData current = movingCredits[i];
            // if this one if 25% of the way down, then we start to send the next one down as well
            current.transform.position = Vector3.MoveTowards(current.transform.position, bottomRect.position, Time.deltaTime * speed);

            float totalDistance = Vector3.Distance(topRect.position, bottomRect.position);
            float distance = Vector3.Distance(current.transform.position, topRect.position);
            float percent = 1 - (totalDistance - distance) / totalDistance;
            
            float alphaTarget = 0;
            if (percent <= 0.125)
                alphaTarget = Mathf.Sin(Mathf.PI * percent * alphaFrequency);
            else if (percent >= 0.875)
                alphaTarget = Mathf.Sin(Mathf.PI * (1 + percent * alphaFrequency));
            else
                alphaTarget = 1;
            creditTextObjects[current.transform].alpha = alphaTarget;

            if (percent >= percentTillNext && !current.hasAdded)
            {
                current.hasAdded = true;
                foreach (CreditData creditData in movingCredits)
                {
                    if (creditData.transform == creditTransforms[transformPointer])
                        return;
                }
                AddNext();
            }
            else if (percent == 1)
            {
                current.transform.position = topRect.transform.position;
                current.transform.gameObject.SetActive(false);
                movingCredits.Remove(current);

                if (creditTransforms[transformPointer] == current.transform)
                    AddNext();
            }
        }
    }
}
