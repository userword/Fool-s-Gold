using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MiniGame
{
    public void Initalize(int dieValue);
    public void OnWin();
    public void OnLoss();
}
