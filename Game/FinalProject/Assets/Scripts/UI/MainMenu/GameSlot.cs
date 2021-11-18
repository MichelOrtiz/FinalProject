using System;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class GameSlot : MonoBehaviour
{
    private SaveFile partida = null;
    private string filePath;
    [SerializeField] private int slot;
    #region UIelements
    [SerializeField]private TextMeshProUGUI lblName;
    [SerializeField]private TextMeshProUGUI lblTime;
    [SerializeField]private TextMeshProUGUI lblZone;
    [SerializeField]private Button btnNewGame;
    [SerializeField]private Button btnLoadGame;
    [SerializeField]private Button btnDeleteGame;
    [SerializeField]private InputCreateGame createInput;
    #endregion
    private void Start() {
        //partida = null;
    }
    void UpdateUI() {
        if(partida!=null){
            //Debug.Log("hay partida en slot " + slot);
            partida.slotFile = slot;
            btnNewGame.gameObject.SetActive(false);
            btnLoadGame.gameObject.SetActive(true);
            btnDeleteGame.gameObject.SetActive(true);
            lblName.gameObject.SetActive(true);
            lblTime.gameObject.SetActive(true);
            lblZone.gameObject.SetActive(true);

            lblName.text = "Nombre: ";
            lblName.text += partida.namePlayer;
            lblTime.text = "Tiempo: ";
            lblTime.text += partida.timeHoursPlayed + ":" + partida.timeMinutesPlayed + ":" + partida.timeSecondsPlayed;
            lblZone.text = "Zona: ";
            switch(partida.sceneToLoad){
                case 1:{
                    lblZone.text += "Playa";
                    break;
                }
                case 2:{
                    lblZone.text += "Pueblo Gnomo";
                    break;
                }
                case 3:{
                    lblZone.text += "Reino Fungi";
                    break;
                }
                case 4:{
                    lblZone.text += "Bazar";
                    break;
                }
                case 5:{
                    lblZone.text += "Dragonsburgo";
                    break;
                }
                case 6:{
                    lblZone.text += "MedufInc";
                    break;
                }
                case 7:{
                    lblZone.text += "Pradera";
                    break;
                }
                case 8:{
                    lblZone.text += "Desierto Zonora";
                    break;
                }
                case 9:{
                    lblZone.text += "Puerta de Hielo";
                    break;
                }
                case 10:{
                    lblZone.text += "Inushima";
                    break;
                }
                case 11:{
                    lblZone.text += "DreamLand";
                    break;
                }
                case 12:{
                    lblZone.text += "Bosque Oscuro";
                    break;
                }
                case 13:{
                    lblZone.text += "Mt. Tsereve";
                    break;
                }
                case 14:{
                    lblZone.text += "Arangentina";
                    break;
                }
                case 15:{
                    lblZone.text += "Blueland";
                    break;
                }
                case 16:{
                    lblZone.text += "Jardin Real";
                    break;
                }
                case 17:{
                    lblZone.text += "Castillo";
                    break;
                }
                default:{
                    lblZone.text += "Desconocida";
                    break;
                }
            }
        }else{
            //Debug.Log("no hay partida en slot " + slot);
            btnNewGame.gameObject.SetActive(true);
            btnLoadGame.gameObject.SetActive(false);
            btnDeleteGame.gameObject.SetActive(false);
            lblName.gameObject.SetActive(false);
            lblTime.gameObject.SetActive(false);
            lblZone.gameObject.SetActive(false);
        }
    }
    public void CreateBtn(){
        createInput.SetGameSlot(this);
        createInput.gameObject.SetActive(true);
    }
    public void CrearPartida(string name){
        partida = new SaveFile(name,slot);
        //partida.controlbinds = KeybindManager.instance.controlbinds;
        partida.controlBindsKeys = KeybindManager.instance.controlbinds.Keys.ToList<string>();
        partida.controlBindsValues = KeybindManager.instance.controlbinds.Values.ToList<KeyCode>();
        SaveFilesManager.instance.WriteSaveFile(partida,filePath);
        UpdateUI();
    }
    public void CargarPartida(){
        //Boy ... cargar cosas
        
        SaveFilesManager.instance.currentSaveSlot = partida;
        SaveFilesManager.instance.SetStartSession(DateTime.Now);
        SceneController.instance.Load(SaveFilesManager.instance.currentSaveSlot);
    }
    public void BorrarPartida(){
        partida = null;
        SaveFilesManager.instance.DeleteSaveFile(filePath);
        UpdateUI();
    }
    public void SetSlot(int slot){
        this.slot = slot;
        filePath = Application.dataPath + "/Partida" + slot;
        partida = SaveFilesManager.instance.LoadSaveFile(filePath);
        UpdateUI();
    }
}
