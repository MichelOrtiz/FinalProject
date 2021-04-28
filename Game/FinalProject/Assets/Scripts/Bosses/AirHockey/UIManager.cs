using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject CanvasGame;
    public GameObject CanvasRestart;
    [Header("CanvasRestart")]
    public GameObject WinTxt;
    public GameObject LooseTxt;
    [Header("Other")]
    public ScoreScript scoreScript;
    public PuckScript puckScript;
    public AirHockeyPlayerMovement airHockeyPlayerMovement;
    public AIScript aIScript;

    public void ShowRestartCanvas(bool AiWon){
        Time.timeScale = 0;
        CanvasGame.SetActive(false);
        CanvasRestart.SetActive(true);
        if (AiWon)
        {
            WinTxt.SetActive(false);
            LooseTxt.SetActive(true);
        }else{
            WinTxt.SetActive(true);
            LooseTxt.SetActive(false);
        }
    }
    public void RestartGame(){
        Time.timeScale = 1;
        CanvasGame.SetActive(true);
        CanvasRestart.SetActive(false);
        scoreScript.ResetScores();
        puckScript.CenterPuck();
        airHockeyPlayerMovement.CenterPosition();
        aIScript.CenterPosition();
    }
}
