using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MBPrecision : MasterMinigame
{
    [SerializeField] private byte scoreToWin;
    void Update()
    {
        if (ScoreController.score >= scoreToWin)
        {
            OnWinMinigame();
        }
    }
}
