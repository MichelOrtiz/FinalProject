using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


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
    public GameObject[] keybindButtons;
    private void Awake()
    {
        
    }
    private void Start()
    {
          
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
        TextMeshProUGUI tmp = Array.Find(keybindButtons, x => x.name == key).GetComponentInChildren<TextMeshProUGUI>();
        if(code == KeyCode.None){
            tmp.text = "Asigna tecla";
        }else{
            tmp.text = code.ToString();
        } 
        if (tmp.text.Length > 6 && tmp.text.Length <= 9)
        {
            tmp.fontSize = 12;
        }else if (tmp.text.Length > 9)
        {
            tmp.fontSize = 8;
        }else
        {
            tmp.fontSize = 16;
        }
    }
}
