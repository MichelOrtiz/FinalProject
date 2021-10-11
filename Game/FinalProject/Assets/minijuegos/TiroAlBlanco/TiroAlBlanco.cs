using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiroAlBlanco : MasterMinigame
{
    [SerializeField] private byte scoreToWin;
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (ScoreController.score >= scoreToWin)
        {
            OnWinMinigame();
        }
    }
}
