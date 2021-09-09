using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance; 
    public static UIManager MyInstance{
        get{
            if (instance==null)
            {
                instance = FindObjectOfType<UIManager>();
            }
            return instance;
        }
    }
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
    private KeyCode action1, action2, action3;
    private GameObject[] keybindButtons;
    private void Awake()
    {
        keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");   
    }
    private void Start()
    {
        //keybindButtons = GameObject.FindGameObjectsWithTag("Keybind");   
    }

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
    public void UpdateKeyText(string key, KeyCode code){
        Text tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<Text>();
        tmp.text = code.ToString();
    }
}
