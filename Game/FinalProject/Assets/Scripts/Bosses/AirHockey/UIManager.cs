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

    [Header("Other")]
    private KeyCode action1, action2, action3;
    public GameObject[] keybindButtons;

    public void UpdateKeyText(string key, KeyCode code)
    {
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
