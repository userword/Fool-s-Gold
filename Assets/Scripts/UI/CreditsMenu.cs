using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    private void Update() {
        if (Input.anyKeyDown)
        {
            UIManager.Singleton.OnAnyKeyCredits();
        }
    }
}
