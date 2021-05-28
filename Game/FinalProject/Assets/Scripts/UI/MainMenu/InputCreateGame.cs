using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputCreateGame : MonoBehaviour
{
    GameSlot gameSlot = null;
    #region UIelements
    [SerializeField] private InputField playerName;
    [SerializeField] private Button btnConfirm;
    [SerializeField] private Button btnReturn;
    #endregion
    private void Start() {
        btnConfirm.enabled=false;
    }
    public void SetGameSlot(GameSlot gameSlot){
        this.gameSlot = gameSlot;
    }
    string GetInputNamePlayer(){
        if(playerName.text!=null){
            return playerName.text;
        }else{
            return "NoName";
        }
    }
    public void InputChanged(){
        if(playerName.text!=""){
            btnConfirm.enabled=true;
        }else{
            btnConfirm.enabled=false;
        }
    }
    public void PressedConfirmBtn(){
        gameSlot.CrearPartida(GetInputNamePlayer());
        gameObject.SetActive(false);
    }
    public void PressedReturnBtn(){
        playerName.text="";
        gameObject.SetActive(false);
    }
    private void OnDisable() {
        playerName.text="";
    }
}
